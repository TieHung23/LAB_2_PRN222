using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EVDMS.Presentation.Pages.Dealer
{
    [Authorize(Roles = "Dealer Staff, Dealer Manager")]
    public class TestDrivesModel : PageModel
    {
        private readonly ITestDriveService _testDriveService;

        public TestDrivesModel(ITestDriveService testDriveService)
        {
            _testDriveService = testDriveService;
        }

        public IEnumerable<TestDrive> TestDriveList { get; set; } = new List<TestDrive>();

        public async Task OnGetAsync()
        {
            var dealerIdClaim = User.FindFirstValue("DealerId");
            if (Guid.TryParse(dealerIdClaim, out Guid dealerId))
            {
                TestDriveList = await _testDriveService.GetByDealerAsync(dealerId);
            }
        }
    }
}