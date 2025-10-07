using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Account;

public class LoginModel : PageModel
{
    [BindProperty] public InputModel Input { get; set; } = null!;

    public string? ReturnUrl { get; set; }

    public void OnGet(string? returnUrl = null)
    {
        ReturnUrl = returnUrl;
    }

    public IActionResult OnPost(string? returnUrl = null)
    {
        // returnUrl ??= Url.Content("~/");

        if (!ModelState.IsValid) return Page();

        if (Input is { Email: "admin@evdms.com", Password: "admin123" })
            return LocalRedirect("/Admin");

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return Page();
    }

    public class InputModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me?")] public bool RememberMe { get; set; }
    }
}