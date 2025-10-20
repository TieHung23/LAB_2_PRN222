using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class TestDrivesModel : PageModel
    {
        public IEnumerable<TestDrive> TestDrives
        {
            get; set;
        }

        private readonly ITestDriveService _testDriveService;
        public TestDrivesModel( ITestDriveService testDriveService )
        {
            _testDriveService = testDriveService;
        }

        public async Task OnGet()
        {
            TestDrives = await _testDriveService.GetAllAsync();
        }
    }
}