using EVDMS.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class ReportsModel : PageModel
    {
        private readonly IPaymentService _paymentService;

        public ReportsModel( IPaymentService paymentService )
        {
            _paymentService = paymentService;
        }

        public class ChartDataViewModel
        {
            public List<string> Labels { get; set; } = new();
            public List<decimal> Data { get; set; } = new();
        }
        [BindProperty]
        public required ChartDataViewModel DealerRevenueChart
        {
            get; set;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var allPayments = await _paymentService.GetAllPaymentAsync();

            var revenueByDealer = allPayments
                .Where( p => p.Order?.Account?.Dealer?.Name != null )
                .GroupBy( p => p.Order!.Account!.Dealer!.Name )
                .Select( group => new
                {
                    DealerName = group.Key,
                    TotalRevenue = group.Sum( p => p.FinalPrice )
                } )
                .OrderByDescending( result => result.TotalRevenue )
                .ToList();
            DealerRevenueChart = new ChartDataViewModel
            {
                Labels = revenueByDealer.Select( d => d.DealerName ).ToList(),
                Data = revenueByDealer.Select( d => d.TotalRevenue ).ToList()
            };

            return Page();
        }
    }
}
