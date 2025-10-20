using EVDMS.Core.Entities;
namespace EVDMS.BLL.Services.Abstractions
{
    public interface ITestDriveService
    {
        Task<TestDrive> CreateTestDriveAsync(TestDrive testDrive);
        Task<IEnumerable<TestDrive>> GetAllAsync();
        Task<TestDrive> GetByIdAsync(Guid id);
        Task UpdateStatusAsync(Guid id, bool isSuccess);
        Task<IEnumerable<TestDrive>> GetByDealerAsync(Guid dealerId);
        Task<TestDrive> CreateAsync(TestDrive testDrive);
    }
}
