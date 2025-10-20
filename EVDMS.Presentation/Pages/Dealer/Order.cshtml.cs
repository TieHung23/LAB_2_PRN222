using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EVDMS.Presentation.Pages.Dealer
{
    public class OrderModel : PageModel
    {
        private readonly IOrderService _orderService;

        public OrderModel( IOrderService orderService )
        {
            _orderService = orderService;
        }

        public IList<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
        public int TotalOrders
        {
            get; set;
        }
        public decimal TotalRevenue
        {
            get; set;
        }


        public async Task OnGetAsync()
        {
            var accountIdClaim = User.FindFirst( ClaimTypes.NameIdentifier )?.Value;
            var dealerIdClaim = User.FindFirst( "DealerId" )?.Value;
            var roleClaim = User.FindFirst( ClaimTypes.Role )?.Value;

            if( accountIdClaim == null || dealerIdClaim == null )
                return;

            Guid.TryParse( accountIdClaim, out Guid accountId );
            Guid.TryParse( dealerIdClaim, out Guid dealerId );

            IEnumerable<Order> orders;

            if( roleClaim == "Dealer Manager" )
            {
                orders = await _orderService.GetOrdersByDealerIdAsync( dealerId );
            }
            else
            {
                orders = await _orderService.GetOrdersByStaffIdAsync( accountId );
            }

            Orders = orders.Select( o => new OrderViewModel
            {
                Id = o.Id,
                CustomerName = o.Customer?.FullName ?? "(Không có)",
                VehicleModel = o.Inventory?.VehicleModel?.ModelName ?? "(N/A)",
                PaymentStatus = o.Payment != null && o.Payment.IsSuccess ? "✅ Hoàn tất" : "❌ Chưa thanh toán",
                FinalPrice = o.Payment?.FinalPrice ?? 0,
                StaffName = o.Account?.FullName ?? "(N/A)",
                CreatedAt = o.CreatedAt
            } ).OrderByDescending( x => x.CreatedAt ).ToList();

            if( roleClaim == "Dealer Manager" )
            {
                TotalOrders = Orders.Count;
                TotalRevenue = Orders.Sum( x => x.FinalPrice );
            }
        }

        public class OrderViewModel
        {
            public Guid Id
            {
                get; set;
            }
            public string CustomerName { get; set; } = string.Empty;
            public string VehicleModel { get; set; } = string.Empty;
            public string StaffName { get; set; } = string.Empty;
            public string PaymentStatus { get; set; } = string.Empty;
            public decimal FinalPrice
            {
                get; set;
            }
            public DateTime CreatedAt
            {
                get; set;
            }
            public decimal TotalRevenue
            {
                get; set;
            }
            public int TotalOrders
            {
                get; set;
            }

        }
    }
}
