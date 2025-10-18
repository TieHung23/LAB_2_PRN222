using EVDMS.Core.Entities;
using EVDMS.DAL.Database;
using EVDMS.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EVDMS.DAL.Repositories.Implementations
{
    public class VehicleConfigRepository : IVehicleConfigRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleConfigRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VehicleConfig>> GetAllAsync()
        {
            // Do VehicleConfig không có VehicleModelId, chúng ta phải include ngược từ VehicleModel
            // để biết nó đang được gán cho Model nào.
            return await _context.VehicleConfigs
                .Include(vc => vc.VehicleModel)
                .Where(vc => !vc.IsDeleted)
                .OrderBy(vc => vc.VersionName)
                .ToListAsync();
        }

        public async Task<VehicleConfig?> GetByIdAsync(Guid id)
        {
            return await _context.VehicleConfigs
                .Include(vc => vc.VehicleModel)
                .FirstOrDefaultAsync(vc => vc.Id == id && !vc.IsDeleted);
        }

        public async Task<VehicleConfig?> GetByVersionNameAsync(string versionName)
        {
            return await _context.VehicleConfigs
                .FirstOrDefaultAsync(vc => vc.VersionName.Equals(versionName, StringComparison.OrdinalIgnoreCase) && !vc.IsDeleted);
        }

        public async Task<VehicleConfig> CreateAsync(VehicleConfig vehicleConfig)
        {
            await _context.VehicleConfigs.AddAsync(vehicleConfig);
            await _context.SaveChangesAsync();
            return vehicleConfig;
        }

        public async Task UpdateAsync(VehicleConfig vehicleConfig)
        {
            _context.VehicleConfigs.Update(vehicleConfig);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(VehicleConfig vehicleConfig)
        {
            // Thực hiện soft-delete
            _context.VehicleConfigs.Update(vehicleConfig);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsInUseByVehicleModel(Guid configId)
        {
            // Kiểm tra xem có VehicleModel nào đang dùng VehicleConfigId này không
            return await _context.VehicleModels.AnyAsync(vm => vm.VehicleConfigId == configId && !vm.IsDeleted);
        }

        public async Task<bool> IsInUseByInventory(Guid configId)
        {
            // Giả sử bảng Inventory có cột VehicleConfigId
            return await _context.Inventories.AnyAsync(i => i.VehicleConfigId == configId);
        }
    }
}
