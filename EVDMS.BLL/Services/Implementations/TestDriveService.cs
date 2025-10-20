using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using EVDMS.DAL.Repositories.Abstractions;

namespace EVDMS.BLL.Services.Implementations
{
    public class TestDriveService : ITestDriveService
    {
        private readonly ITestDriveRepository _testDriveRepository;
        public TestDriveService(ITestDriveRepository testDriveRepository)
        {
            _testDriveRepository = testDriveRepository;
        }
        public async Task<TestDrive> CreateTestDriveAsync(TestDrive testDrive)
        {
            return await _testDriveRepository.AddAsync(testDrive);
        }

        public async Task<IEnumerable<TestDrive>> GetAllAsync()
        {
            return await _testDriveRepository.GetAllAsync();
        }

        public async Task<TestDrive> GetByIdAsync(Guid id)
        {
            return await _testDriveRepository.GetByIdAsync(id);
        }

        public async Task UpdateStatusAsync(Guid id, bool isSuccess)
        {
            var testDrive = await _testDriveRepository.GetByIdAsync(id);
            if (testDrive != null)
            {
                testDrive.IsSuccess = isSuccess;
                await _testDriveRepository.UpdateAsync(testDrive);
            }
        }

        public async Task<IEnumerable<TestDrive>> GetByDealerAsync(Guid dealerId)
        {
            return await _testDriveRepository.GetByDealerAsync(dealerId);
        }

        public async Task<TestDrive> CreateAsync(TestDrive testDrive)
        {
            return await _testDriveRepository.AddAsync(testDrive);
        }
    }
}