using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> OnPostUpdateStatusAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return Page();
            }

            // Gọi service đã có sẵn để cập nhật IsSuccess = true
            await _testDriveService.UpdateStatusAsync(id, true);

            TempData["SuccessMessage"] = "Cập nhật trạng thái lịch hẹn thành công!";
            return RedirectToPage();
        }
    }
}