using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVDMS.Core.Entities;
namespace EVDMS.DAL.Repositories.Abstractions
{
    public interface IVehicleModelRepository
    {
        Task<IEnumerable<VehicleModel>> GetAllAsync(string? searchTerm);
        Task<VehicleModel?> GetByIdAsync(Guid id);
        Task<VehicleModel?> GetByNameAsync(string modelName);
        Task<VehicleModel> CreateAsync(VehicleModel vehicleModel);
        Task UpdateAsync(VehicleModel vehicleModel);
        Task DeleteAsync(VehicleModel vehicleModel);
        Task<IEnumerable<VehicleModel>> SearchAsync(string searchTerm);
    }
}