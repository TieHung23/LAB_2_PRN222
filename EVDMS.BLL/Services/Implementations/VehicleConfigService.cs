using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using EVDMS.DAL.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDMS.BLL.Services.Implementations
{
    public class VehicleConfigService : IVehicleConfigService
    {
        private readonly IVehicleConfigRepository _configRepository;
        private readonly IVehicleModelRepository _modelRepository; // Cần để cập nhật VehicleModel

        public VehicleConfigService(IVehicleConfigRepository configRepository, IVehicleModelRepository modelRepository)
        {
            _configRepository = configRepository;
            _modelRepository = modelRepository;
        }

        public async Task<IEnumerable<VehicleConfig>> GetAllAsync()
        {
            return await _configRepository.GetAllAsync();
        }

        public async Task<VehicleConfig?> GetByIdAsync(Guid id)
        {
            return await _configRepository.GetByIdAsync(id);
        }

        public async Task<VehicleConfig> CreateAndAssignAsync(VehicleConfig vehicleConfig, Guid vehicleModelId)
        {
            ValidateVehicleConfig(vehicleConfig);

            // 1. Kiểm tra VersionName không được trùng trên toàn hệ thống
            var existingByName = await _configRepository.GetByVersionNameAsync(vehicleConfig.VersionName);
            if (existingByName != null)
            {
                throw new InvalidOperationException($"Lỗi: Tên phiên bản '{vehicleConfig.VersionName}' đã tồn tại.");
            }

            // 2. Tìm VehicleModel để gán Config vào
            var modelToAssign = await _modelRepository.GetByIdAsync(vehicleModelId);
            if (modelToAssign == null)
            {
                throw new KeyNotFoundException($"Lỗi: Không tìm thấy mẫu xe với ID '{vehicleModelId}' để gán cấu hình.");
            }

            // 3. Tạo mới VehicleConfig
            vehicleConfig.Id = Guid.NewGuid();
            vehicleConfig.IsDeleted = false;
            var newConfig = await _configRepository.CreateAsync(vehicleConfig);

            // 4. Gán ID của Config mới tạo vào VehicleModel và cập nhật
            modelToAssign.VehicleConfigId = newConfig.Id;
            await _modelRepository.UpdateAsync(modelToAssign);

            return newConfig;
        }

        public async Task UpdateAsync(Guid id, VehicleConfig vehicleConfig)
        {
            if (id != vehicleConfig.Id)
            {
                throw new ArgumentException("Lỗi: ID của cấu hình không khớp.");
            }

            var existingConfig = await _configRepository.GetByIdAsync(id);
            if (existingConfig == null)
            {
                throw new KeyNotFoundException($"Lỗi: Không tìm thấy cấu hình với ID '{id}'.");
            }

            ValidateVehicleConfig(vehicleConfig);

            // Nếu đổi tên, kiểm tra tên mới có bị trùng với một config khác không
            if (!existingConfig.VersionName.Equals(vehicleConfig.VersionName, StringComparison.OrdinalIgnoreCase))
            {
                var existingByName = await _configRepository.GetByVersionNameAsync(vehicleConfig.VersionName);
                if (existingByName != null && existingByName.Id != existingConfig.Id)
                {
                    throw new InvalidOperationException($"Lỗi: Tên phiên bản '{vehicleConfig.VersionName}' đã tồn tại.");
                }
            }

            // Cập nhật các thuộc tính
            existingConfig.VersionName = vehicleConfig.VersionName;
            existingConfig.Color = vehicleConfig.Color;
            existingConfig.InteriorType = vehicleConfig.InteriorType;
            existingConfig.BasePrice = vehicleConfig.BasePrice;
            existingConfig.WarrantyPeriod = vehicleConfig.WarrantyPeriod;
            existingConfig.UpdatedAt = DateTime.UtcNow;

            await _configRepository.UpdateAsync(existingConfig);
        }

        public async Task DeleteAsync(Guid id)
        {
            var configToDelete = await _configRepository.GetByIdAsync(id);
            if (configToDelete == null)
            {
                throw new KeyNotFoundException($"Lỗi: Không tìm thấy cấu hình với ID '{id}'.");
            }

            // Kiểm tra xem config có đang được gán cho model nào không
            if (await _configRepository.IsInUseByVehicleModel(id))
            {
                throw new InvalidOperationException($"Không thể xóa cấu hình '{configToDelete.VersionName}' vì nó đang được gán cho một mẫu xe.");
            }

            // Kiểm tra xem config có trong kho không
            if (await _configRepository.IsInUseByInventory(id))
            {
                throw new InvalidOperationException($"Không thể xóa cấu hình '{configToDelete.VersionName}' vì đang có xe trong kho.");
            }

            configToDelete.IsDeleted = true;
            configToDelete.UpdatedAt = DateTime.UtcNow;

            await _configRepository.DeleteAsync(configToDelete);
        }

        private void ValidateVehicleConfig(VehicleConfig vehicleConfig)
        {
            if (vehicleConfig == null)
                throw new ArgumentNullException(nameof(vehicleConfig), "Dữ liệu cấu hình không được để trống.");
            if (string.IsNullOrWhiteSpace(vehicleConfig.VersionName))
                throw new ArgumentException("Tên phiên bản là trường bắt buộc.", nameof(vehicleConfig.VersionName));
            if (vehicleConfig.BasePrice <= 0)
                throw new ArgumentException("Giá cơ bản phải là số dương.", nameof(vehicleConfig.BasePrice));
        }
    }
}
