using EVDMS.Core.Entities;
using EVDMS.DAL.Database;
using EVDMS.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EVDMS.DAL.Repositories.Implementations
{
    public class TestDriveRepository : ITestDriveRepository
    {
        private readonly ApplicationDbContext _context;
        public TestDriveRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<TestDrive> AddAsync(TestDrive testDrive)
        {
            await _context.TestDrives.AddAsync(testDrive);
            await _context.SaveChangesAsync();
            return testDrive;
        }

        public async Task<IEnumerable<TestDrive>> GetAllAsync()
        {
            return await _context.TestDrives
                                 .Include(td => td.Customer)
                                 .Include(td => td.VehicleModel)
                                 .Where(td => !td.IsDeleted)
                                 .OrderByDescending(td => td.ScheduledDateTime)
                                 .ToListAsync();
        }

        public async Task<TestDrive> GetByIdAsync(Guid id)
        {
            return await _context.TestDrives
                                 .Include(td => td.Customer)
                                 .Include(td => td.VehicleModel)
                                 .FirstOrDefaultAsync(td => td.Id == id);
        }
        public async Task<IEnumerable<TestDrive>> GetByDealerAsync(Guid dealerId)
        {
            var vehicleModelIdsAtDealer = await _context.Inventories
                .Where(i => i.DealerId == dealerId)
                .Select(i => i.VehicleModelId)
                .Distinct()
                .ToListAsync();

            return await _context.TestDrives
                .Where(td => vehicleModelIdsAtDealer.Contains(td.VehicleModelId))
                .Include(td => td.Customer)
                .Include(td => td.VehicleModel)
                .OrderByDescending(td => td.ScheduledDateTime)
                .ToListAsync();
        }
        public async Task UpdateAsync(TestDrive testDrive)
        {
            _context.TestDrives.Update(testDrive);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsSlotAvailableAsync(DateTime appointmentTime)
        {
            var startTime = appointmentTime;
            var endTime = startTime.AddHours(1);

            // Tìm bất kỳ lịch hẹn nào *chưa hoàn thành* (IsSuccess == false) bị trùng lặp
            var existingAppointment = await _context.TestDrives
                .FirstOrDefaultAsync(td =>
                    !td.IsSuccess && 
                    td.ScheduledDateTime.HasValue &&
                    td.ScheduledDateTime.Value < endTime && 
                    td.ScheduledDateTime.Value.AddHours(1) > startTime 
                );

            return existingAppointment == null; 
        }
    }
}
