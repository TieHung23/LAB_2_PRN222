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

        // ... (Các hàm GetAllAsync, GetByIdAsync, GetByVersionNameAsync giữ nguyên) ...
        public async Task<IEnumerable<VehicleConfig>> GetAllAsync()
        {
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
                .FirstOrDefaultAsync(vc => vc.VersionName.ToUpper() == versionName.ToUpper() && !vc.IsDeleted); 
        }


        // Hàm CreateAsync cũ (nếu có)
        public async Task<VehicleConfig> CreateAsync(VehicleConfig vehicleConfig)
        {
            await _context.VehicleConfigs.AddAsync(vehicleConfig);
            await _context.SaveChangesAsync();
            return vehicleConfig;
        }

        // --- THÊM PHẦN TRIỂN KHAI CHO HÀM MỚI ---
        public async Task<VehicleConfig?> AddAndReturnAsync(VehicleConfig config)
        {
            if (config == null) return null;

            await _context.VehicleConfigs.AddAsync(config);
            await _context.SaveChangesAsync();
            return config; // Trả về đối tượng đã được thêm (với ID được gán)
        }
        // ------------------------------------------

        // ... (Các hàm UpdateAsync, DeleteAsync, IsInUse... giữ nguyên) ...
        public async Task UpdateAsync(VehicleConfig vehicleConfig)
        {
            _context.VehicleConfigs.Update(vehicleConfig);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(VehicleConfig vehicleConfig)
        {
            // Thực hiện soft-delete
            vehicleConfig.IsDeleted = true; // Đảm bảo flag IsDeleted được set
            _context.VehicleConfigs.Update(vehicleConfig);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsInUseByVehicleModel(Guid configId)
        {
            return await _context.VehicleModels.AnyAsync(vm => vm.VehicleConfigId == configId && !vm.IsDeleted);
        }

        public async Task<bool> IsInUseByInventory(Guid configId)
        {
            // Sửa lại theo cấu trúc Inventory: Kiểm tra qua VehicleModelId
            // Lấy danh sách Model ID dùng Config này
            var modelIdsUsingConfig = await _context.VehicleModels
                                              .Where(vm => vm.VehicleConfigId == configId && !vm.IsDeleted)
                                              .Select(vm => vm.Id)
                                              .ToListAsync();

            if (!modelIdsUsingConfig.Any())
            {
                return false; // Không có Model nào dùng Config này thì chắc chắn không có trong Kho
            }

            // Kiểm tra xem có Inventory nào dùng các Model ID này không
            return await _context.Inventories
                                 .AnyAsync(i => modelIdsUsingConfig.Contains(i.VehicleModelId));
        }

    }
}