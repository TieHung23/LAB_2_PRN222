using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class OrdersModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;

        public OrdersModel( IOrderService orderService, IPaymentService paymentService )
        {
            _orderService = orderService;
            _paymentService = paymentService;
        }

        // Thay đổi kiểu dữ liệu của thuộc tính
        public IEnumerable<OrderViewModel> Orders
        {
            get; set;
        }

        // Bind các tham số từ query string để filter và search
        [BindProperty( SupportsGet = true )]
        public string SearchTerm
        {
            get; set;
        }

        [BindProperty( SupportsGet = true )]
        public string StatusFilter
        {
            get; set;
        }

        public async Task OnGetAsync()
        {
            var allOrders = await _orderService.GetAllOrder();
            var allPayments = await _paymentService.GetAllPaymentAsync();

            // 2. Dùng LINQ để JOIN hai danh sách này lại
            var query = from order in allOrders
                        join payment in allPayments on order.Id equals payment.OrderId
                        select new OrderViewModel
                        {
                            OrderId = order.Id,
                            OrderDate = order.CreatedAt,
                            CustomerFullName = order.Customer?.FullName,
                            CustomerEmail = order.Customer?.Email,
                            DealerName = order.Account?.Dealer.Name,
                            VehicleModelName = order.Inventory?.VehicleModel.ModelName,
                            VehicleColor = order.Inventory?.VehicleModel.VehicleConfig.Color,
                            TotalAmount = payment.FinalPrice,
                            Status = payment.IsSuccess ? "Success" : "Failed"
                        };

            if( !string.IsNullOrEmpty( StatusFilter ) )
            {
                query = query.Where( o => o.Status == StatusFilter );
            }

            if( !string.IsNullOrEmpty( SearchTerm ) )
            {
                query = query.Where( o => o.OrderId.ToString().Contains( SearchTerm ) ||
                                         ( o.CustomerFullName != null && o.CustomerFullName.Contains( SearchTerm, StringComparison.OrdinalIgnoreCase ) ) );
            }
            Orders = query.ToList();
        }
    }
}