using EVDMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVDMS.BLL.Services.Abstractions
{
    public interface IVehicleConfigService
    {
        Task<IEnumerable<VehicleConfig>> GetAllAsync();
        Task<VehicleConfig?> GetByIdAsync(Guid id);
        Task<VehicleConfig> CreateAndAssignAsync(VehicleConfig vehicleConfig, Guid vehicleModelId);
        Task UpdateAsync(Guid id, VehicleConfig vehicleConfig);
        Task DeleteAsync(Guid id);
        Task<VehicleConfig?> AddAndReturnAsync(VehicleConfig config);
        Task UpdateVehicleConfigAsync(VehicleConfig config);
    }
}
