using EVDMS.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; 
using System.Linq;

namespace EVDMS.Presentation.Pages.Admin.Accounts
{
    public class EditModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        private readonly IDealerService _dealerService;

        // --- Hằng số vai trò (giống CreateModel) ---
        private const string AdminRoleName = "Admin";
        private const string EvmStaffRoleName = "EVM Staff";
        private const string DealerManagerRoleName = "Dealer Manager";
        private const string DealerStaffRoleName = "Dealer Staff";

        public EditModel(IAccountService accountService, IRoleService roleService, IDealerService dealerService)
        {
            _accountService = accountService;
            _roleService = roleService;
            _dealerService = dealerService;
        }

        // BindProperty cho Account đang sửa
        [BindProperty]
        public EVDMS.Core.Entities.Account Account { get; set; } = new();

        // Không cần BindProperty cho Password ở trang Edit

        public SelectList Roles { get; set; }
        public SelectList Dealers { get; set; }

        // Hàm OnGet để tải dữ liệu account cần sửa
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound(); // Không có ID thì báo lỗi
            }

            // Lấy account từ DB, kèm theo Role để kiểm tra
            Account = await _accountService.GetAccountByIdWithDetailsAsync(id.Value);

            if (Account == null)
            {
                return NotFound(); // Không tìm thấy account
            }

            // --- Không cho phép sửa tài khoản Admin ---
            if (Account.Role?.Name.Equals(AdminRoleName, StringComparison.OrdinalIgnoreCase) ?? false)
            {
                // Có thể redirect về trang danh sách với thông báo lỗi
                TempData["ErrorMessage"] = "Cannot edit the Admin account.";
                return RedirectToPage("../Accounts");
            }

            await LoadDropdownData(); // Tải dropdown sau khi đã có Account
            return Page();
        }

        // Hàm OnPost để xử lý cập nhật
        public async Task<IActionResult> OnPostAsync()
        {
            // Tải lại dropdown trước khi validation
            await LoadDropdownData();

            // Lấy thông tin Role được chọn để kiểm tra
            var selectedRole = (await _roleService.GetAllAsync()).FirstOrDefault(r => r.Id == Account.RoleId);

            // --- VALIDATION TƯƠNG TỰ CREATE ---
            if (selectedRole != null)
            {
                // 1. Không cho phép đổi thành Admin (dù dropdown đã lọc)
                if (selectedRole.Name.Equals(AdminRoleName, StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("Account.RoleId", "Cannot change account role to Admin.");
                }
                // 2. Bắt buộc chọn Dealer cho vai trò Dealer
                else if ((selectedRole.Name.Equals(DealerManagerRoleName, StringComparison.OrdinalIgnoreCase) ||
                         selectedRole.Name.Equals(DealerStaffRoleName, StringComparison.OrdinalIgnoreCase))
                         && Account.DealerId == null)
                {
                    ModelState.AddModelError("Account.DealerId", "Dealer selection is required for Dealer roles.");
                }
                // 3. Tự động set DealerId = null cho vai trò không thuộc Dealer
                else if (!selectedRole.Name.Equals(DealerManagerRoleName, StringComparison.OrdinalIgnoreCase) &&
                         !selectedRole.Name.Equals(DealerStaffRoleName, StringComparison.OrdinalIgnoreCase))
                {
                    Account.DealerId = null;
                }
            }
            else if (Account.RoleId != Guid.Empty)
            {
                ModelState.AddModelError("Account.RoleId", "Invalid Role selected.");
            }

            if (!ModelState.IsValid)
            {
                var originalAccount = await _accountService.GetAccountByIdWithDetailsAsync(Account.Id);
                if (originalAccount != null)
                {
                    Account.Email = originalAccount.Email; // Đảm bảo email đúng
                    Account.HashedPassword = originalAccount.HashedPassword; 
                }
                return Page();
            }

            // --- LOGIC UPDATE ---
            try
            {
                // Lấy entity gốc từ DB để cập nhật, AsNoTracking để tránh lỗi tracking
                var accountToUpdate = await _accountService.GetAccountByIdAsync(Account.Id);

                if (accountToUpdate == null)
                {
                    return NotFound();
                }

                // --- Không cho phép sửa tài khoản Admin  ---
                var originalRole = await _roleService.GetByIdAsync(accountToUpdate.RoleId);
                if (originalRole?.Name.Equals(AdminRoleName, StringComparison.OrdinalIgnoreCase) ?? false)
                {
                    TempData["ErrorMessage"] = "Cannot edit the Admin account.";
                    return RedirectToPage("../Accounts");
                }


                
                accountToUpdate.FullName = Account.FullName;
                accountToUpdate.RoleId = Account.RoleId;
                accountToUpdate.DealerId = Account.DealerId; 
                accountToUpdate.IsActive = Account.IsActive;
               

                await _accountService.UpdateAccountAsync(accountToUpdate);

                TempData["SuccessMessage"] = $"Account '{accountToUpdate.Email}' updated successfully!";
                return RedirectToPage("../Accounts"); // Redirect về danh sách
            }
            catch (DbUpdateConcurrencyException) 
            {
                ModelState.AddModelError(string.Empty, "The account was modified by another user. Please reload and try again.");
              
                Account = await _accountService.GetAccountByIdWithDetailsAsync(Account.Id);
                if (Account == null) return NotFound();
                await LoadDropdownData();
                return Page();
            }
            catch (Exception ex)
            {
               
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                // Tải lại dữ liệu gốc
                Account = await _accountService.GetAccountByIdWithDetailsAsync(Account.Id);
                if (Account == null) return NotFound();
                await LoadDropdownData();
                return Page();
            }
        }

        
        private async Task LoadDropdownData()
        {
            var allRoles = await _roleService.GetAllAsync();
            
            var creatableRoles = allRoles.Where(r => !r.Name.Equals(AdminRoleName, StringComparison.OrdinalIgnoreCase));

            var dealers = await _dealerService.GetAllAsync();

            Roles = new SelectList(creatableRoles, "Id", "Name", Account?.RoleId);
            Dealers = new SelectList(dealers, "Id", "Name", Account?.DealerId);
        }
    }
}