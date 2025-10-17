using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
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

        public IEnumerable<Customer> Customers { get; set; }

        public async Task OnGetAsync()
        {
            // Placeholder:
            // Customers = await _customerService.GetAllAsync();
            Customers = new List<Customer>();
        }
    }
}