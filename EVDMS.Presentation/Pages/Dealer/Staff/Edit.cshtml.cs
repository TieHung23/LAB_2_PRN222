using EVDMS.BLL.Services.Abstractions;
using AccountEntity = EVDMS.Core.Entities.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EVDMS.Presentation.Pages.Dealer.Staff
{
    public class EditModel : PageModel
    {
        private readonly IAccountService _accountService;

        public EditModel(IAccountService accountService)
        {
            _accountService = accountService;
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var existingStaff = await _accountService.GetAccountByIdAsync(Staff.Id);
            if (existingStaff == null)
                return NotFound();

            // Ensure the manager can only edit staff from the same dealer
            var dealerIdClaim = User.FindFirst("DealerId")?.Value;
            if (dealerIdClaim == null || existingStaff.DealerId.ToString() != dealerIdClaim)
            {
                return Forbid();
            }

            existingStaff.FullName = Staff.FullName;
            existingStaff.Email = Staff.Email;
            existingStaff.IsActive = Staff.IsActive;

            await _accountService.UpdateAccountAsync(existingStaff);

            TempData["SuccessMessage"] = "Staff information has been updated successfully!";
            return RedirectToPage("/Dealer/Staff/Index");
        }
    }
}
