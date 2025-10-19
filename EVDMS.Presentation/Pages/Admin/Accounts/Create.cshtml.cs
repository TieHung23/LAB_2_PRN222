using EVDMS.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EVDMS.Presentation.Pages.Admin.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly IRoleService _roleService;
        private readonly IDealerService _dealerService;

        private const string AdminRoleName = "Admin";
        private const string DealerManagerRoleName = "Dealer Manager";
        private const string DealerStaffRoleName = "Dealer Staff";


        public CreateModel(IAccountService accountService, IRoleService roleService, IDealerService dealerService)
        {
            _accountService = accountService;
            _roleService = roleService;
            _dealerService = dealerService;
        }

        [BindProperty]
        public EVDMS.Core.Entities.Account Account { get; set; } = new();

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }

        public SelectList Roles { get; set; }
        public SelectList Dealers { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadDropdownData();
            Account.IsActive = true;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await LoadDropdownData();

            var selectedRole = (await _roleService.GetAllAsync()).FirstOrDefault(r => r.Id == Account.RoleId);
            EVDMS.Core.Entities.Dealer? selectedDealer = null;

            // --- VALIDATION NÂNG CAO (Giữ nguyên) ---
            if (Account.DealerId.HasValue)
            {
                selectedDealer = await _dealerService.GetDealerByIdAsync(Account.DealerId.Value);
                if (selectedDealer == null || !selectedDealer.IsActive)
                {
                    ModelState.AddModelError("Account.DealerId", "Selected dealer is invalid or inactive.");
                    Account.DealerId = null;
                }
            }
            if (selectedRole != null)
            {
                if (selectedRole.Name.Equals(AdminRoleName, StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("Account.RoleId", "Cannot create Admin accounts using this form.");
                }
                else if ((selectedRole.Name.Equals(DealerManagerRoleName, StringComparison.OrdinalIgnoreCase) ||
                         selectedRole.Name.Equals(DealerStaffRoleName, StringComparison.OrdinalIgnoreCase))
                         && Account.DealerId == null)
                {
                    if (!ModelState.ContainsKey("Account.DealerId"))
                    {
                        ModelState.AddModelError("Account.DealerId", "Active Dealer selection is required for Dealer roles.");
                    }
                }
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
            if (await _accountService.EmailExistsAsync(Account.Email))
            {
                ModelState.AddModelError("Account.Email", "This email address is already in use.");
            }
            // --- KẾT THÚC VALIDATION NÂNG CAO ---

            // *** THÊM KIỂM TRA PASSWORD RỖNG TRƯỚC KHI HASH ***
            if (string.IsNullOrWhiteSpace(Password))
            {
                // Thêm lỗi này dù đã có [Required], để chắc chắn
                ModelState.AddModelError("Password", "Password cannot be empty.");
            }
            // ************************************************

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Chỉ hash nếu Password không rỗng (dù ModelState đã valid)
                if (!string.IsNullOrWhiteSpace(Password))
                {
                    Account.HashedPassword = BCrypt.Net.BCrypt.HashPassword(Password);
                }
                else
                {
                    // Trường hợp này không nên xảy ra nếu ModelState.IsValid,
                    // nhưng thêm để phòng ngừa
                    ModelState.AddModelError(string.Empty, "Password hashing failed due to empty input.");
                    return Page();
                }

                await _accountService.CreateAccountAsync(Account);

                TempData["SuccessMessage"] = $"Account '{Account.Email}' created successfully!";
                return RedirectToPage("../Accounts");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return Page();
            }
        }

        private async Task LoadDropdownData()
        {
            var allRoles = await _roleService.GetAllAsync();
            var creatableRoles = allRoles.Where(r => !r.Name.Equals(AdminRoleName, StringComparison.OrdinalIgnoreCase));

            var allDealers = await _dealerService.GetAllAsync();
            var activeDealers = allDealers.Where(d => d.IsActive);

            Roles = new SelectList(creatableRoles, "Id", "Name", Account?.RoleId);
            Dealers = new SelectList(activeDealers, "Id", "Name", Account?.DealerId);
        }
    }
}