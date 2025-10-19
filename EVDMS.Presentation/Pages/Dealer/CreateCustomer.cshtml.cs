using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace EVDMS.Presentation.Pages.Dealer
{
    [Authorize(Roles = "Dealer Staff, Dealer Manager")]
    public class CreateCustomerModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CreateCustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [BindProperty]
        public CustomerInput Input { get; set; } = new CustomerInput();

        public class CustomerInput
        {
            [Required(ErrorMessage = "Vui lòng nhập họ tên")]
            [Display(Name = "Họ và tên")]
            public string FullName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Vui lòng nhập SĐT")]
            [Phone]
            [Display(Name = "Số điện thoại")]
            public string PhoneNumber { get; set; } = string.Empty;

            [Required(ErrorMessage = "Vui lòng nhập Email")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; } = string.Empty;

            [Display(Name = "Địa chỉ")]
            public string? Address { get; set; }

            [Display(Name = "Số CMND/CCCD")]
            public string? IdCardNumber { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var staffAccountId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var newCustomer = new Customer
            {
                FullName = Input.FullName,
                PhoneNumber = Input.PhoneNumber,
                Email = Input.Email,
                Address = Input.Address,
                IdCardNumber = Input.IdCardNumber,
                CreatedById = staffAccountId // Gán ID của nhân viên tạo
            };

            await _customerService.CreateCustomerAsync(newCustomer);

            TempData["SuccessMessage"] = "Tạo khách hàng mới thành công!";
            return RedirectToPage("/Dealer/Customers");
        }
    }
}