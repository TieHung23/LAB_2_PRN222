using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EVDMS.Presentation.Pages.Admin.Customers
{
    public class EditModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public EditModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [BindProperty]
        public Customer Customer { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customer = await _customerService.GetByIdAsync(id.Value);

            if (Customer == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // --- VALIDATION TRÙNG LẶP (loại trừ chính customer đang sửa) ---
            if (await _customerService.EmailExistsAsync(Customer.Email, Customer.Id))
            {
                ModelState.AddModelError("Customer.Email", "Customer Email already exists.");
            }
            if (await _customerService.PhoneNumberExistsAsync(Customer.PhoneNumber, Customer.Id))
            {
                ModelState.AddModelError("Customer.PhoneNumber", "Customer Phone Number already exists.");
            }
            if (await _customerService.IdCardNumberExistsAsync(Customer.IdCardNumber, Customer.Id))
            {
                ModelState.AddModelError("Customer.IdCardNumber", "Customer ID Card Number already exists.");
            }
            // --- KẾT THÚC VALIDATION ---

            if (!ModelState.IsValid)
            {
                return Page(); // Trả về form với lỗi
            }

            // --- LOGIC UPDATE ---
            try
            {

                await _customerService.UpdateCustomerAsync(Customer);


                TempData["SuccessMessage"] = $"Customer '{Customer.FullName}' updated successfully!";
                return RedirectToPage("../Customers"); // Redirect về danh sách
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, "The customer profile was modified by another user. Please reload and try again.");
                return Page();
            }
            catch (Exception ex)
            {
                // Log lỗi
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return Page();
            }
        }
    }
}