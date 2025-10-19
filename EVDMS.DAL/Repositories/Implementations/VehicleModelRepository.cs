using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVDMS.Core.Entities;
using EVDMS.DAL.Database;
using EVDMS.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EVDMS.DAL.Repositories.Implementations
{
    public class VehicleModelRepository : IVehicleModelRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VehicleModel?> GetModelWithConfigByIdAsync(Guid id)
        {
            return await _context.VehicleModels
                .Include(vm => vm.VehicleConfig) // Quan trọng: Lấy cả Config đi kèm
                .FirstOrDefaultAsync(vm => vm.Id == id && !vm.IsDeleted);
        }
        public async Task<IEnumerable<VehicleModel>> GetAllAsync()
        {
            return await _context.VehicleModels
                                 .Include(vm => vm.VehicleConfig)
                                 .Where(vm => !vm.IsDeleted)
                                 .OrderBy(vm => vm.ModelName)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<VehicleModel>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Enumerable.Empty<VehicleModel>();
            }

            var lowerCaseSearchTerm = searchTerm.ToLower();

            return await _context.VehicleModels
                .Include(vm => vm.VehicleConfig)
                .Where(vm => !vm.IsDeleted &&
                             (vm.ModelName.ToLower().Contains(lowerCaseSearchTerm) ||
                              vm.VehicleType.ToLower().Contains(lowerCaseSearchTerm)))
                .OrderBy(vm => vm.ModelName)
                .ToListAsync();
        }

        public async Task<VehicleModel?> GetByIdAsync(Guid id)
        {
            return await _context.VehicleModels
                                 .Include(vm => vm.VehicleConfig)
                                 .FirstOrDefaultAsync(vm => vm.Id == id && !vm.IsDeleted);
        }

        public async Task<VehicleModel?> GetByNameAsync(string modelName)
        {
            return await _context.VehicleModels
        .FirstOrDefaultAsync(vm => vm.ModelName.ToUpper() == modelName.ToUpper() && !vm.IsDeleted); 
        }

        public async Task<VehicleModel> CreateAsync(VehicleModel vehicleModel)
        {
            await _context.VehicleModels.AddAsync(vehicleModel);
            await _context.SaveChangesAsync();
            return vehicleModel;
        }

        public async Task UpdateAsync(VehicleModel vehicleModel)
        {
            _context.VehicleModels.Update(vehicleModel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(VehicleModel vehicleModel)
        {
            _context.VehicleModels.Update(vehicleModel); // Dùng Update để thực hiện soft delete
            await _context.SaveChangesAsync();
        }

        public async Task<VehicleModel?> GetByModelAndVersionAsync(string modelName, string versionName)
        {
            // Chuyển sang ToUpper() để so sánh không phân biệt hoa thường
            var modelNameUpper = modelName.ToUpper();
            var versionNameUpper = versionName.ToUpper();

            return await _context.VehicleModels
                .Include(vm => vm.VehicleConfig) // Phải Include VehicleConfig để check
                .FirstOrDefaultAsync(vm =>
                    vm.ModelName.ToUpper() == modelNameUpper &&
                    vm.VehicleConfig.VersionName.ToUpper() == versionNameUpper &&
                    !vm.IsDeleted);
        }
    }
}