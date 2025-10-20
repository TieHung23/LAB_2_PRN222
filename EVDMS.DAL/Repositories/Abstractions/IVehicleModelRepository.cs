using EVDMS.Core.Entities;
namespace EVDMS.DAL.Repositories.Abstractions
{
    public interface IVehicleModelRepository
    {
        Task<IEnumerable<VehicleModel>> GetAllAsync();
        Task<IEnumerable<VehicleModel>> SearchAsync(string searchTerm);
        Task<VehicleModel?> GetByIdAsync(Guid id);
        Task<VehicleModel?> GetByNameAsync(string modelName);
        Task<VehicleModel> CreateAsync(VehicleModel vehicleModel);
        Task UpdateAsync(VehicleModel vehicleModel);
        Task DeleteAsync(VehicleModel vehicleModel);

        Task<VehicleModel?> GetModelWithConfigByIdAsync(Guid id);

        Task<VehicleModel?> GetByModelAndVersionAsync(string modelName, string versionName);
    }
}