using EVDMS.BLL.Services.Abstractions;
// Sửa lỗi 1: Chỉ định rõ chúng ta đang dùng Entity 'Account'
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class AccountsModel : PageModel
    {
        private readonly IAccountService _accountService;
        // Bạn cũng có thể inject IRoleService và IDealerService để lấy dữ liệu cho dropdowns

        public AccountsModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // Sửa lỗi 1: Chỉ định rõ class 'Account'
        public IEnumerable<EVDMS.Core.Entities.Account> Accounts { get; set; }

        [BindProperty]
        // Sửa lỗi 1: Chỉ định rõ class 'Account'
        public EVDMS.Core.Entities.Account Account { get; set; }

        // Thêm các Property để giữ danh sách Roles và Dealers cho Modal
        // public IEnumerable<Role> Roles { get; set; }
        // public IEnumerable<Dealer> Dealers { get; set; }

        public async Task OnGetAsync()
        {
            // Placeholder: Thay thế bằng lệnh gọi service thực tế
            // Accounts = await _accountService.GetAllAsync(); 
            // Roles = await _roleService.GetAllAsync();
            // Dealers = await _dealerService.GetAllAsync();

            Accounts = new List<EVDMS.Core.Entities.Account>(); // Khởi tạo rỗng để trang không lỗi
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Nếu model không hợp lệ, tải lại trang với dữ liệu đã nhập
                await OnGetAsync(); // Tải lại danh sách cho dropdowns
                return Page();
            }

            // Placeholder: Thêm logic để Create hoặc Update
            // if (Account.Id == Guid.Empty) // So sánh Guid
            // {
            //    await _accountService.CreateAsync(Account);
            // }
            // else
            // {
            //    await _accountService.UpdateAsync(Account);
            // }

            return RedirectToPage();
        }
    }
}