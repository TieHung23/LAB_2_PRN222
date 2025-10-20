using EVDMS.BLL.Services.Abstractions;
using AccountEntity = EVDMS.Core.Entities.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EVDMS.Presentation.Pages.Dealer.Staff
{
    public class DeleteModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IDealerService _dealerService;

        public DeleteModel(IAccountService accountService, IDealerService dealerService)
        {
            _accountService = accountService;
            _dealerService = dealerService;
        }

        [BindProperty]
        public AccountEntity Staff { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Staff = await _accountService.GetAccountByIdAsync(id);
            if (Staff == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            var dealerIdClaim = User.FindFirst("DealerId")?.Value;
            if (dealerIdClaim == null || !Guid.TryParse(dealerIdClaim, out Guid dealerGuid))
            {
                ModelState.AddModelError(string.Empty, "Không xác định được đại lý hiện tại.");
                return Page();
            }

            var staff = await _accountService.GetAccountByIdAsync(id);
            if (staff == null)
                return NotFound();

            // ✅ Kiểm tra xem nhân viên có cùng DealerId hay không
            if (staff.DealerId != dealerGuid)
                return Forbid();

            // ✅ Soft Delete
            staff.IsDeleted = true;
            staff.IsActive = false;

            await _accountService.UpdateAccountAsync(staff);

            TempData["SuccessMessage"] = $"Nhân viên '{staff.FullName}' đã được xóa thành công.";
            return RedirectToPage("/Dealer/Staff/Index");
        }
    }
}
