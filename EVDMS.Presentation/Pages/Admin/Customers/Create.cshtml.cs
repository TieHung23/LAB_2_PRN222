using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin.Customers
{
    public class CreateModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CreateModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [BindProperty]
        public Customer Customer { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // --- VALIDATION  ---
            if (await _customerService.EmailExistsAsync(Customer.Email))
            {
                ModelState.AddModelError("Customer.Email", "Customer Email already exists.");
            }
            if (await _customerService.PhoneNumberExistsAsync(Customer.PhoneNumber))
            {
                ModelState.AddModelError("Customer.PhoneNumber", "Customer Phone Number already exists.");
            }
            if (await _customerService.IdCardNumberExistsAsync(Customer.IdCardNumber))
            {
                ModelState.AddModelError("Customer.IdCardNumber", "Customer ID Card Number already exists.");
            }
            // --- KẾT THÚC VALIDATION  ---


            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Gọi service để tạo Customer
                await _customerService.CreateCustomerAsync(Customer);

                // Thêm thông báo thành công vào TempData
                TempData["SuccessMessage"] = $"Customer '{Customer.FullName}' created successfully!";

                // Redirect về trang danh sách Customers
                return RedirectToPage("../Customers");
            }
            catch (Exception ex)
            {
                // Log lỗi
                ModelState.AddModelError(string.Empty, $"An error occurred while creating the customer: {ex.Message}");
                return Page(); // Trả về trang với thông báo lỗi chung
            }
        }
    }
}