using EVDMS.BLL.Services.Abstractions;
using AccountEntity = EVDMS.Core.Entities.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EVDMS.Presentation.Pages.Dealer.Staff
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IDealerService _dealerService;

        public CreateModel(IAccountService accountService, IDealerService dealerService)
        {
            _accountService = accountService;
            _dealerService = dealerService;
        }

        [BindProperty]
        public AccountEntity Staff { get; set; } = new();

        public void OnGet()
        {
            Staff.IsActive = true;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // ✅ 1. Kiểm tra ModelState
            if (!ModelState.IsValid)
                return Page();

            // ✅ 2. Kiểm tra trùng email
            if (await _accountService.EmailExistsAsync(Staff.Email))
            {
                ModelState.AddModelError("Staff.Email", "Email này đã được sử dụng trong hệ thống.");
                return Page();
            }

            // ✅ 3. Lấy DealerId từ user đang đăng nhập
            var dealerIdClaim = User.FindFirst("DealerId")?.Value;
            if (string.IsNullOrEmpty(dealerIdClaim) || !Guid.TryParse(dealerIdClaim, out Guid dealerGuid))
            {
                ModelState.AddModelError(string.Empty, "Không xác định được đại lý hiện tại.");
                return Page();
            }

            // ✅ 4. Kiểm tra Dealer có tồn tại không
            var dealer = await _dealerService.GetDealerByIdAsync(dealerGuid);
            if (dealer == null)
            {
                ModelState.AddModelError(string.Empty, "Đại lý không tồn tại hoặc đã bị xóa.");
                return Page();
            }

            // ✅ 5. Gán thông tin cho Account mới
            Staff.DealerId = dealer.Id; // DealerId kiểu Guid?
            Staff.HashedPassword = BCrypt.Net.BCrypt.HashPassword("123456"); // Mật khẩu mặc định
            Staff.IsDeleted = false;
            Staff.IsActive = true;


            var dealerStaffRole = await _accountService.GetRoleByNameAsync("Dealer Staff");
            if (dealerStaffRole == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy Role DealerStaff trong hệ thống.");
                return Page();
            }

            Staff.RoleId = dealerStaffRole.Id;

            // ✅ 6. Thêm nhân viên mới
            await _accountService.CreateAccountAsync(Staff);

            TempData["SuccessMessage"] = $"Nhân viên '{Staff.FullName}' đã được thêm thành công!";
            return RedirectToPage("/Dealer/Staff/Index");
        }
    }
}
