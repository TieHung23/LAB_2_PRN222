using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class OrdersModel : PageModel
    {
        private readonly IOrderService _orderService;

        public OrdersModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IEnumerable<Order> Orders { get; set; }

        public async Task OnGetAsync()
        {
            // Placeholder:
            // Orders = await _orderService.GetAllOrdersAsync();
            Orders = new List<Order>();
        }
    }
}