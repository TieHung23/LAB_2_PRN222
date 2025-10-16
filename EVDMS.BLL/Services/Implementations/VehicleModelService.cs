using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using EVDMS.DAL.Repositories.Abstractions;
using EVDMS.DAL.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDMS.BLL.Services.Implementations
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IVehicleModelRepository _vehicleModelRepository;
        private readonly IInventoryRepository _inventoryRepository;

        public VehicleModelService(IVehicleModelRepository vehicleModelRepository, IInventoryRepository inventoryRepository)
        {
            _vehicleModelRepository = vehicleModelRepository;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<VehicleModel>> GetAllAsync(string? searchTerm)
        {
            return await _vehicleModelRepository.GetAllAsync(searchTerm);
        }

        public async Task<VehicleModel?> GetByIdAsync(Guid id)
        {
            return await _vehicleModelRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<VehicleModel>> SearchAsync(string searchTerm)
        {
            return await _vehicleModelRepository.SearchAsync(searchTerm);
        }

        public async Task<VehicleModel> CreateAsync(VehicleModel vehicleModel)
        {
            ValidateVehicleModel(vehicleModel);

            var existingByName = await _vehicleModelRepository.GetByNameAsync(vehicleModel.ModelName);
            if (existingByName != null)
            {
                throw new InvalidOperationException($"Lỗi: Tên mẫu xe '{vehicleModel.ModelName}' đã tồn tại trong hệ thống.");
            }

            vehicleModel.Id = Guid.NewGuid();
            vehicleModel.IsActive = true;
            vehicleModel.IsDeleted = false;
            vehicleModel.CreatedAt = DateTime.UtcNow;

            return await _vehicleModelRepository.CreateAsync(vehicleModel);
        }

        public async Task UpdateAsync(Guid id, VehicleModel vehicleModel)
        {
            if (id != vehicleModel.Id)
            {
                throw new ArgumentException("Lỗi: ID của xe không khớp. Không thể thực hiện cập nhật.");
            }

            ValidateVehicleModel(vehicleModel);

            var existingVehicle = await _vehicleModelRepository.GetByIdAsync(id);
            if (existingVehicle == null)
            {
                throw new KeyNotFoundException($"Lỗi: Không tìm thấy mẫu xe với ID '{id}' để cập nhật.");
            }

            if (!existingVehicle.ModelName.Equals(vehicleModel.ModelName, StringComparison.OrdinalIgnoreCase))
            {
                var existingByName = await _vehicleModelRepository.GetByNameAsync(vehicleModel.ModelName);
                if (existingByName != null)
                {
                    throw new InvalidOperationException($"Lỗi: Tên mẫu xe '{vehicleModel.ModelName}' đã tồn tại trong hệ thống.");
                }
            }

            existingVehicle.ModelName = vehicleModel.ModelName;
            existingVehicle.Brand = vehicleModel.Brand;
            existingVehicle.VehicleType = vehicleModel.VehicleType;
            existingVehicle.Description = vehicleModel.Description;
            existingVehicle.ImgUrl = vehicleModel.ImgUrl;
            existingVehicle.ReleaseYear = vehicleModel.ReleaseYear;
            existingVehicle.IsActive = vehicleModel.IsActive;

            await _vehicleModelRepository.UpdateAsync(existingVehicle);
        }

        public async Task DeleteAsync(Guid id)
        {
            var vehicleToDelete = await _vehicleModelRepository.GetByIdAsync(id);
            if (vehicleToDelete == null)
            {
                throw new KeyNotFoundException($"Lỗi: Không tìm thấy mẫu xe với ID '{id}' để xóa.");
            }

            var isVehicleInAnyInventory = await _inventoryRepository.CheckIfVehicleModelExistsInInventory(id);
            if (isVehicleInAnyInventory)
            {
                throw new InvalidOperationException($"Không thể xóa mẫu xe '{vehicleToDelete.ModelName}' vì vẫn còn xe trong kho của đại lý. Vui lòng xóa hết xe trong kho trước.");
            }

            vehicleToDelete.IsDeleted = true;
            vehicleToDelete.IsActive = false;

            await _vehicleModelRepository.DeleteAsync(vehicleToDelete);
        }

        /// <summary>
        /// Phương thức hỗ trợ để kiểm tra dữ liệu đầu vào của VehicleModel
        /// </summary>
        private void ValidateVehicleModel(VehicleModel vehicleModel)
        {
            if (vehicleModel == null)
            {
                throw new ArgumentNullException(nameof(vehicleModel), "Dữ liệu xe không được để trống.");
            }
            if (string.IsNullOrWhiteSpace(vehicleModel.ModelName))
            {
                throw new ArgumentException("Tên mẫu xe (ModelName) là trường bắt buộc.", nameof(vehicleModel.ModelName));
            }
            if (string.IsNullOrWhiteSpace(vehicleModel.Brand))
            {
                throw new ArgumentException("Thương hiệu (Brand) là trường bắt buộc.", nameof(vehicleModel.Brand));
            }
            if (string.IsNullOrWhiteSpace(vehicleModel.VehicleType))
            {
                throw new ArgumentException("Loại xe (VehicleType) là trường bắt buộc.", nameof(vehicleModel.VehicleType));
            }
            if (vehicleModel.ReleaseYear <= 1900 || vehicleModel.ReleaseYear > DateTime.UtcNow.Year + 1)
            {
                throw new ArgumentException($"Năm ra mắt '{vehicleModel.ReleaseYear}' không hợp lệ.", nameof(vehicleModel.ReleaseYear));
            }
        }
    }
}