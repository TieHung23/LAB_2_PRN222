using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.CommonEntities;
using EVDMS.Core.Entities;
using EVDMS.DAL.Repositories.Abstractions;

namespace EVDMS.BLL.Services.Implementations
{
    public class VehicleConfigService : IVehicleConfigService
    {
        // 1. Đảm bảo dòng này tồn tại và đúng chính tả
        private readonly IVehicleConfigRepository _vehicleConfigRepository;

        // 2. Đảm bảo constructor nhận đúng tham số interface
        public VehicleConfigService(IVehicleConfigRepository vehicleConfigRepository /*, IVehicleModelRepository modelRepository */)
        {
            // 3. Đảm bảo dòng gán này tồn tại và đúng chính tả
            _vehicleConfigRepository = vehicleConfigRepository;
            // _modelRepository = modelRepository;
        }

        public async Task<IEnumerable<VehicleConfig>> GetAllAsync()
        {
            return await _vehicleConfigRepository.GetAllAsync();
        }

        public async Task<VehicleConfig?> GetByIdAsync(Guid id)
        {
            return await _vehicleConfigRepository.GetByIdAsync(id);
        }

        public async Task<VehicleConfig?> AddAndReturnAsync(VehicleConfig config)
        {
            // Validate VehicleConfig if needed here
            // Example Validation:
            if (string.IsNullOrWhiteSpace(config.VersionName))
                throw new ArgumentException("Version name cannot be empty.", nameof(config.VersionName));
            if (string.IsNullOrWhiteSpace(config.Color))
                throw new ArgumentException("Color cannot be empty.", nameof(config.Color));

            // ---- ĐÃ COMMENT LẠI THEO HƯỚNG DẪN TRƯỚC ----
            /*
            var existingByName = await _vehicleConfigRepository.GetByVersionNameAsync(config.VersionName);
            if (existingByName != null)
            {
                throw new InvalidOperationException($"Tên phiên bản '{config.VersionName}' đã tồn tại.");
            }
            */
            // ---- HẾT PHẦN COMMENT ----

            config.CreatedAt = DateTime.UtcNow;
            config.CreatedAtTick = DateTime.UtcNow.Ticks;

            // Đảm bảo rằng bạn đã inject _vehicleConfigRepository
            return await _vehicleConfigRepository.AddAndReturnAsync(config);
        }

        public async Task UpdateVehicleConfigAsync(VehicleConfig config)
        {
            // Validate VehicleConfig if needed here
            var existingConfig = await _vehicleConfigRepository.GetByIdAsync(config.Id);
            if (existingConfig == null)
            {
                throw new KeyNotFoundException($"Config with ID {config.Id} not found.");
            }
            // Check for duplicate VersionName (excluding self)
            var existingByName = await _vehicleConfigRepository.GetByVersionNameAsync(config.VersionName);
            if (existingByName != null && existingByName.Id != config.Id)
            {
                throw new InvalidOperationException($"Another config with Version Name '{config.VersionName}' already exists.");
            }

            await _vehicleConfigRepository.UpdateAsync(config);
        }


        public async Task DeleteAsync(Guid id)
        {
            var configToDelete = await _vehicleConfigRepository.GetByIdAsync(id);
            if (configToDelete == null)
            {
                throw new KeyNotFoundException($"Lỗi: Không tìm thấy cấu hình với ID '{id}'.");
            }

            // Kiểm tra xem config có đang được gán cho model nào không
            if (await _vehicleConfigRepository.IsInUseByVehicleModel(id))
            {
                throw new InvalidOperationException($"Không thể xóa cấu hình '{configToDelete.VersionName}' vì nó đang được gán cho một mẫu xe.");
            }

            // Kiểm tra xem config có trong kho không
            if (await _vehicleConfigRepository.IsInUseByInventory(id))
            {
                throw new InvalidOperationException($"Không thể xóa cấu hình '{configToDelete.VersionName}' vì đang có xe trong kho.");
            }

            configToDelete.IsDeleted = true;
            // Giả sử Config kế thừa UpdatedCommon
            if (configToDelete is UpdatedCommon updatedCommonConfig)
            {
                updatedCommonConfig.UpdatedAt = DateTime.UtcNow;
                updatedCommonConfig.UpdatedAtTick = DateTime.UtcNow.Ticks;
                // Cần thêm UpdatedById nếu có logic
            }


            await _vehicleConfigRepository.DeleteAsync(configToDelete); // Sử dụng tên đúng (hàm này thực ra là update để soft-delete)
        }

        // Giữ lại hàm Validate (nếu có)
        private void ValidateVehicleConfig(VehicleConfig vehicleConfig)
        {
            //... validation logic ...
        }

        // Cần thêm hàm CreateAndAssignAsync đã có trong file bạn gửi lên
        public async Task<VehicleConfig> CreateAndAssignAsync(VehicleConfig vehicleConfig, Guid vehicleModelId)
        {
            ValidateVehicleConfig(vehicleConfig);

            var existingByName = await _vehicleConfigRepository.GetByVersionNameAsync(vehicleConfig.VersionName);
            if (existingByName != null)
            {
                throw new InvalidOperationException($"Tên phiên bản '{vehicleConfig.VersionName}' đã tồn tại.");
            }

            // Gán VehicleModelId nếu cần (mặc dù VehicleConfig không có trường này)
            // Logic này có vẻ không cần thiết nếu VehicleModel mới là cái giữ VehicleConfigId

            var createdConfig = await _vehicleConfigRepository.CreateAsync(vehicleConfig);

            // Cập nhật VehicleModel để liên kết với Config mới? 
            // Logic này nên nằm ở VehicleModelService thì đúng hơn.
            // var model = await _modelRepository.GetByIdAsync(vehicleModelId);
            // if (model != null)
            // {
            //     model.VehicleConfigId = createdConfig.Id;
            //     await _modelRepository.UpdateAsync(model);
            // } else {
            //     // Handle error: Model not found
            // }


            return createdConfig;
        }

        // Cần thêm hàm UpdateAsync cũ (nếu logic khác UpdateVehicleConfigAsync)
        public async Task UpdateAsync(Guid id, VehicleConfig vehicleConfig)
        {
            if (id != vehicleConfig.Id)
            {
                throw new ArgumentException("ID trong URL và ID trong dữ liệu không khớp.");
            }

            ValidateVehicleConfig(vehicleConfig);

            var configToUpdate = await _vehicleConfigRepository.GetByIdAsync(id);
            if (configToUpdate == null)
            {
                throw new KeyNotFoundException($"Lỗi: Không tìm thấy cấu hình với ID '{id}'.");
            }

            // Kiểm tra trùng tên (ngoại trừ chính nó)
            var existingByName = await _vehicleConfigRepository.GetByVersionNameAsync(vehicleConfig.VersionName);
            if (existingByName != null && existingByName.Id != id)
            {
                throw new InvalidOperationException($"Tên phiên bản '{vehicleConfig.VersionName}' đã tồn tại.");
            }


            // Cập nhật các trường cần thiết
            configToUpdate.VersionName = vehicleConfig.VersionName;
            configToUpdate.Color = vehicleConfig.Color;
            configToUpdate.InteriorType = vehicleConfig.InteriorType;
            configToUpdate.BasePrice = vehicleConfig.BasePrice;
            configToUpdate.WarrantyPeriod = vehicleConfig.WarrantyPeriod;
            // KHÔNG cập nhật IsDeleted ở đây
            if (configToUpdate is UpdatedCommon updatedCommonConfig)
            {
                updatedCommonConfig.UpdatedAt = DateTime.UtcNow;
                updatedCommonConfig.UpdatedAtTick = DateTime.UtcNow.Ticks;
                // Cần thêm UpdatedById nếu có logic
            }


            await _vehicleConfigRepository.UpdateAsync(configToUpdate);
        }

    }
}