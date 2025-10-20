using EVDMS.Core.Entities;
namespace EVDMS.BLL.Services.Abstractions
{
    public interface IVehicleModelService
    {
        Task<IEnumerable<VehicleModel>> GetAllAsync();
        Task<IEnumerable<VehicleModel>> SearchAsync(string searchTerm);
        Task<VehicleModel?> GetByIdAsync(Guid id);
        Task<VehicleModel> CreateAsync(VehicleModel vehicleModel);
        Task UpdateAsync(Guid id, VehicleModel vehicleModel);
        Task DeleteAsync(Guid id);
        Task<VehicleModel?> GetModelWithConfigByIdAsync(Guid id);

        Task UpdateVehicleModelAsync(VehicleModel model);
    }
}