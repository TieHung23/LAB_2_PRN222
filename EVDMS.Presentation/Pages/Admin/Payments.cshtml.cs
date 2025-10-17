using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class PaymentsModel : PageModel
    {
        private readonly IOrderService _orderService; // Payment logic có thể nằm trong OrderService

        public PaymentsModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IEnumerable<Payment> Payments { get; set; }

        public async Task OnGetAsync()
        {
            // Placeholder:
            // Payments = await _orderService.GetAllPaymentsAsync();
            Payments = new List<Payment>();
        }
    }
}