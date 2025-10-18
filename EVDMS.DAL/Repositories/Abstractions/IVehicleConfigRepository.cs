using EVDMS.Core.Entities;

namespace EVDMS.DAL.Repositories.Abstractions
{
    public interface IVehicleConfigRepository
    {
        Task<IEnumerable<VehicleConfig>> GetAllAsync();

        Task<VehicleConfig?> GetByIdAsync(Guid id);

        Task<VehicleConfig?> GetByVersionNameAsync(string versionName);

        Task<VehicleConfig> CreateAsync(VehicleConfig vehicleConfig);

        Task UpdateAsync(VehicleConfig vehicleConfig);

        Task DeleteAsync(VehicleConfig vehicleConfig);

        // Kiểm tra xem Config có đang được gán cho Model nào không
        Task<bool> IsInUseByVehicleModel(Guid configId);

        // Kiểm tra xem Config có đang tồn tại trong kho không
        Task<bool> IsInUseByInventory(Guid configId);
    }
}
