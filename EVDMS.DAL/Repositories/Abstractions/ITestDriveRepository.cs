using EVDMS.Core.Entities;
namespace EVDMS.DAL.Repositories.Abstractions
{
    public interface ITestDriveRepository
    {
        Task<TestDrive> AddAsync(TestDrive testDrive);
        Task<IEnumerable<TestDrive>> GetAllAsync();
        Task<TestDrive> GetByIdAsync(Guid id);
        Task UpdateAsync(TestDrive testDrive);
        Task<IEnumerable<TestDrive>> GetByDealerAsync(Guid dealerId);

        Task<bool> IsSlotAvailableAsync(DateTime appointmentTime);
    }
}
