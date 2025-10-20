using EVDMS.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class AccountsModel : PageModel
    {
        private readonly IAccountService _accountService;
        // Xóa RoleService và DealerService vì không cần load dropdown ở đây nữa

        // Inject IAccountService
        public AccountsModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // Chỉ cần danh sách Accounts để hiển thị
        public IEnumerable<EVDMS.Core.Entities.Account> Accounts { get; set; } = new List<EVDMS.Core.Entities.Account>();

        // Xóa các BindProperty liên quan đến modal

        public async Task OnGetAsync()
        {
            // Chỉ cần lấy danh sách tài khoản (đã bao gồm Role và Dealer nếu service làm đúng)
            Accounts = await _accountService.GetAccounts(string.Empty);
        }

        // Xóa OnPostAsync (logic Create/Update đã chuyển sang trang khác)

        // Giữ lại OnPostDeleteAsync
        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            try
            {
                await _accountService.DeleteAccountAsync(id);
                // (Tùy chọn) Thêm TempData để hiển thị thông báo thành công
                // TempData["SuccessMessage"] = "Account deleted successfully.";
            }
            catch (Exception ex)
            {
                // Thêm log lỗi
                // (Tùy chọn) Thêm TempData để hiển thị thông báo lỗi
                // TempData["ErrorMessage"] = $"Error deleting account: {ex.Message}";
            }
            return RedirectToPage("./Accounts"); // Redirect về trang danh sách
        }

        // Xóa hàm LoadModalData()
    }
}