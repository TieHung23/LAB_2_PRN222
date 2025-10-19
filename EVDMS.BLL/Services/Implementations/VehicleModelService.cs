using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using EVDMS.DAL.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EVDMS.BLL.Services.Implementations
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IVehicleModelRepository _vehicleModelRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IVehicleConfigRepository _vehicleConfigRepository; 

        public VehicleModelService(
            IVehicleModelRepository vehicleModelRepository,
            IInventoryRepository inventoryRepository,
            IVehicleConfigRepository vehicleConfigRepository) 
        {
            _vehicleModelRepository = vehicleModelRepository;
            _inventoryRepository = inventoryRepository;
            _vehicleConfigRepository = vehicleConfigRepository; 
        }

        public async Task<IEnumerable<VehicleModel>> GetAllAsync()
        {
            return await _vehicleModelRepository.GetAllAsync();
        }

        public async Task<IEnumerable<VehicleModel>> SearchAsync(string searchTerm)
        {
            return await _vehicleModelRepository.SearchAsync(searchTerm);
        }

        public async Task<VehicleModel?> GetByIdAsync(Guid id)
        {
            return await _vehicleModelRepository.GetByIdAsync(id);
        }

        public async Task<VehicleModel?> GetModelWithConfigByIdAsync(Guid id)
        {
            return await _vehicleModelRepository.GetModelWithConfigByIdAsync(id);
        }

        public async Task<VehicleModel> CreateAsync(VehicleModel vehicleModel)
        {
            ValidateVehicleModel(vehicleModel);

            vehicleModel.CreatedAt = DateTime.UtcNow;
            vehicleModel.CreatedAtTick = DateTime.UtcNow.Ticks;

            return await _vehicleModelRepository.CreateAsync(vehicleModel);
        }

        public async Task UpdateAsync(Guid id, VehicleModel vehicleModel)
        {
            // Hàm này có thể không còn cần thiết vì chúng ta dùng UpdateVehicleModelAsync
            // Nhưng chúng ta sẽ giữ nó
            var modelToUpdate = await _vehicleModelRepository.GetByIdAsync(id);
            if (modelToUpdate == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy mẫu xe với ID '{id}'.");
            }

            modelToUpdate.ModelName = vehicleModel.ModelName;
            await _vehicleModelRepository.UpdateAsync(modelToUpdate);
        }

        public async Task UpdateVehicleModelAsync(VehicleModel model)
        {
            // Hàm này được gọi từ Edit.cshtml.cs
            await _vehicleModelRepository.UpdateAsync(model);
        }

        public async Task DeleteAsync(Guid id)
        {
            var vehicleToDelete = await _vehicleModelRepository.GetByIdAsync(id);
            if (vehicleToDelete == null)
            {
                throw new KeyNotFoundException($"Lỗi: Không tìm thấy mẫu xe với ID '{id}' để xóa.");
            }

            // Kiểm tra xem xe có trong kho không (logic này cần được hoàn thiện sau)
            // var inventoryInUse = await _inventoryRepository.CheckIfVehicleModelInStock(id);
            // if(inventoryInUse)
            // {
            //    throw new InvalidOperationException("Không thể xóa mẫu xe đang có trong kho.");
            // }

            // Lấy Config liên quan
            var configToDelete = await _vehicleConfigRepository.GetByIdAsync(vehicleToDelete.VehicleConfigId);

            // Thực hiện Soft-Delete cho cả hai
            vehicleToDelete.IsDeleted = true;
            vehicleToDelete.IsActive = false;
            await _vehicleModelRepository.DeleteAsync(vehicleToDelete); // Soft-delete Model

            if (configToDelete != null)
            {
                configToDelete.IsDeleted = true;
                await _vehicleConfigRepository.DeleteAsync(configToDelete); // Soft-delete Config
            }
        }

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
            if (vehicleModel.ReleaseYear <= 1900 || vehicleModel.ReleaseYear > DateTime.UtcNow.Year + 2)
            {
                throw new ArgumentOutOfRangeException(nameof(vehicleModel.ReleaseYear), $"Năm ra mắt '{vehicleModel.ReleaseYear}' không hợp lệ.");
            }
        }
    }
}