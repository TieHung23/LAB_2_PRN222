using System.ComponentModel.DataAnnotations;
using EVDMS.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

// Cần cho .Where()

namespace EVDMS.Presentation.Pages.Admin.Accounts;

public class CreateModel : PageModel
{
    // --- Dùng hằng số để tránh lỗi chính tả ---
    private const string AdminRoleName = "Admin";
    private const string EvmStaffRoleName = "EVM Staff";
    private const string DealerManagerRoleName = "Dealer Manager";
    private const string DealerStaffRoleName = "Dealer Staff";
    private readonly IAccountService _accountService;
    private readonly IDealerService _dealerService;
    private readonly IRoleService _roleService;


    public CreateModel(IAccountService accountService, IRoleService roleService, IDealerService dealerService)
    {
        _accountService = accountService;
        _roleService = roleService;
        _dealerService = dealerService;
    }

    [BindProperty] public Core.Entities.Account Account { get; set; } = new();

    [BindProperty]
    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")] // Thêm validation độ dài
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

        // --- LOGIC VALIDATION  ---
        if (selectedRole != null)
        {
            // 1. Không cho phép tạo Admin
            if (selectedRole.Name.Equals(AdminRoleName, StringComparison.OrdinalIgnoreCase))
                ModelState.AddModelError("Account.RoleId", "Cannot create Admin accounts using this form.");
            // 2. Bắt buộc chọn Dealer cho vai trò Dealer
            else if ((selectedRole.Name.Equals(DealerManagerRoleName, StringComparison.OrdinalIgnoreCase) ||
                      selectedRole.Name.Equals(DealerStaffRoleName, StringComparison.OrdinalIgnoreCase))
                     && Account.DealerId == null)
                ModelState.AddModelError("Account.DealerId", "Dealer selection is required for Dealer roles.");
            // 3. Tự động set DealerId = null cho vai trò không thuộc Dealer
            else if (!selectedRole.Name.Equals(DealerManagerRoleName, StringComparison.OrdinalIgnoreCase) &&
                     !selectedRole.Name.Equals(DealerStaffRoleName, StringComparison.OrdinalIgnoreCase))
                Account.DealerId = null;
        }
        else if (Account.RoleId != Guid.Empty)
        {
            ModelState.AddModelError("Account.RoleId", "Invalid Role selected.");
        }

        // (Validation Email )
        if (await _accountService.EmailExistsAsync(Account.Email))
            ModelState.AddModelError("Account.Email", "This email address is already in use.");


        if (!ModelState.IsValid) return Page();

        try
        {
            Account.HashedPassword = Password;
            await _accountService.CreateAccountAsync(Account);

            TempData["SuccessMessage"] = $"Account '{Account.Email}' created successfully! '{Account.HashedPassword}'";
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

        var dealers = await _dealerService.GetAllAsync();


        Roles = new SelectList(creatableRoles, "Id", "Name", Account?.RoleId);
        Dealers = new SelectList(dealers, "Id", "Name", Account?.DealerId);
    }
}