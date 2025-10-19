using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EVDMS.BLL.Services.Abstractions;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace EVDMS.Presentation.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;

        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [BindProperty]
        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var basicAccount = await _accountService.Login(Email, Password);

            if (basicAccount != null)
            {
                var accountWithDetails = await _accountService.GetAccountByIdWithDetailsAsync(basicAccount.Id);

                if (accountWithDetails == null || accountWithDetails.Role == null)
                {
                    Message = "Tài khoản không có vai trò (Role) hợp lệ. Vui lòng liên hệ Admin.";
                    return Page();
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, accountWithDetails.Id.ToString()),
                    new Claim(ClaimTypes.Email, accountWithDetails.Email),
                    new Claim(ClaimTypes.Name, accountWithDetails.FullName),
                    new Claim(ClaimTypes.Role, accountWithDetails.Role.Name)
                };

                if (accountWithDetails.DealerId.HasValue)
                {
                    claims.Add(new Claim("DealerId", accountWithDetails.DealerId.Value.ToString()));
                }

                var claimsIdentity = new ClaimsIdentity(claims, "MyCookieAuth");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync("MyCookieAuth", claimsPrincipal);

                //  LOGIC CHUYỂN HƯỚNG 
                string userRole = accountWithDetails.Role.Name;

                if (userRole == "Admin")
                {
                    return RedirectToPage("/Admin/Index"); 
                }

                if (userRole == "Dealer Staff" || userRole == "Dealer Manager")
                {
                    return RedirectToPage("/Dealer/Index");
                }

                return RedirectToPage("/Index");
            }

            Message = "Email hoặc mật khẩu không chính xác.";
            return Page();
        }
    }
}