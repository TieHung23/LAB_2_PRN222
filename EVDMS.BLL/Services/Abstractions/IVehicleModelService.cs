using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}