using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class CustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomersModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IEnumerable<Customer> Customers { get; set; } = new List<Customer>(); 

        public async Task OnGetAsync()
        {
            // Lấy danh sách khách hàng
            Customers = await _customerService.GetAllAsync();
        }

        // Thêm Handler Delete
        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(id);
                TempData["SuccessMessage"] = "Customer deleted successfully.";
            }
            catch (Exception ex)
            {
                // Log lỗi
                TempData["ErrorMessage"] = $"Error deleting customer: {ex.Message}";
                // Xử lý cụ thể lỗi khóa ngoại nếu cần
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    TempData["ErrorMessage"] = "Cannot delete customer because they have existing orders or test drives.";
                }
            }
            return RedirectToPage("./Customers");
        }
    }
}