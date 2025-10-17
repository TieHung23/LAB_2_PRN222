using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class TestDrivesModel : PageModel
    {
        // Danh sách để hiển thị lên giao diện
        public IEnumerable<TestDrive> TestDrives { get; set; }

        // Bạn có thể inject service của mình ở đây khi sẵn sàng
        // private readonly ITestDriveService _testDriveService;
        // public TestDrivesModel(ITestDriveService testDriveService)
        // {
        //     _testDriveService = testDriveService;
        // }

        public TestDrivesModel()
        {
            TestDrives = new List<TestDrive>();
        }

        public void OnGet()
        {
            // Khi bạn có service, bạn sẽ gọi:
            // TestDrives = _testDriveService.GetAll(); 
            // (Hãy chắc chắn .Include(t => t.Customer).Include(t => t.VehicleModel))

            // Dữ liệu giả để hiển thị (giao diện đang hard-code)
            // Bạn sẽ thay thế bằng dữ liệu thật sau
            var customer1 = new Customer { FullName = "John Smith", PhoneNumber = "090-123-4567" };
            var customer2 = new Customer { FullName = "Maria Garcia", PhoneNumber = "091-222-3333" };
            var model1 = new VehicleModel { ModelName = "VinFast VF8 Eco" };
            var model2 = new VehicleModel { ModelName = "VinFast VF9 Plus" };

            TestDrives = new List<TestDrive>
            {
                new TestDrive
                {
                    Id = Guid.NewGuid(),
                    Customer = customer1,
                    VehicleModel = model1,
                    ScheduledDateTime = new DateTime(2025, 10, 18, 10, 0, 0),
                    IsSuccess = false,
                    IsActive = true
                },
                new TestDrive
                {
                    Id = Guid.NewGuid(),
                    Customer = customer2,
                    VehicleModel = model2,
                    ScheduledDateTime = new DateTime(2025, 10, 17, 14, 0, 0),
                    IsSuccess = true,
                    IsActive = true
                }
            };
        }
    }
}