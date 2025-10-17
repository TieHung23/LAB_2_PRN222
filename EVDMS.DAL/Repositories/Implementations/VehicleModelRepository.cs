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
        public async Task<VehicleModel> CreateAsync(VehicleModel vehicleModel)
        {
            await _context.VehicleModels.AddAsync(vehicleModel);
            await _context.SaveChangesAsync();
            return vehicleModel;
        }
        public async Task DeleteAsync(VehicleModel vehicleModel)
        {
            _context.VehicleModels.Update(vehicleModel);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<VehicleModel>> GetAllAsync(string? searchTerm)
        {
            var query = _context.VehicleModels.Where(vm => !vm.IsDeleted);

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(vm => vm.ModelName.Contains(searchTerm));
            }

            return await query.OrderBy(vm => vm.ModelName).ToListAsync();
        }
        public async Task<VehicleModel?> GetByIdAsync(Guid id)
        {
            return await _context.VehicleModels.FirstOrDefaultAsync(vm => vm.Id == id && !vm.IsDeleted);
        }
        public async Task<VehicleModel?> GetByNameAsync(string modelName)
        {
            return await _context.VehicleModels.FirstOrDefaultAsync(vm =>
                vm.ModelName.Equals(modelName, StringComparison.OrdinalIgnoreCase) && !vm.IsDeleted);
        }
        public async Task UpdateAsync(VehicleModel vehicleModel)
        {
            _context.VehicleModels.Update(vehicleModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<VehicleModel>> SearchAsync(string searchTerm)
        {
            // Nếu từ khóa rỗng hoặc null, trả về danh sách trống
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Enumerable.Empty<VehicleModel>();
            }

            var lowerCaseSearchTerm = searchTerm.ToLower();

            return await _context.VehicleModels
                .Where(vm => !vm.IsDeleted &&
                             (vm.ModelName.ToLower().Contains(lowerCaseSearchTerm) ||
                              vm.VehicleType.ToLower().Contains(lowerCaseSearchTerm)))
                .OrderBy(vm => vm.ModelName)
                .ToListAsync();
        }
    }
}