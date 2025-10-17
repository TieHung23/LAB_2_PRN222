using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin;

[Authorize(Roles = "Admin")]
public class IndexModel : PageModel
{
    private readonly IAccountService _accountService;
    private readonly ICustomerService _customerService;
    private readonly IOrderService _orderService;
    private readonly IVehicleModelService _vehicleModelService;

    public IndexModel(IAccountService accountService,
        ICustomerService customerService,
        IOrderService orderService,
        IVehicleModelService vehicleModelService)
    {
        _accountService = accountService;
        _customerService = customerService;
        _orderService = orderService;
        _vehicleModelService = vehicleModelService;
    }

    [BindProperty] public List<AdminIndexResponse> AdminIndexResponses { get; set; } = new();

    public async Task OnGetAsync()
    {
        try
        {
            var customers = await _customerService.GetAllAsync();
            var vehicles = await _vehicleModelService.GetAllAsync(null);
            var orders = await _orderService.GetAllOrder();
            var accounts = await _accountService.GetAccounts(null!);

            var grouped = (from c in customers
                group c by new { c.CreatedAt.Year, c.CreatedAt.Month }
                into g
                select new
                {
                    g.Key.Year,
                    g.Key.Month,
                    NewCustomerCount = g.Count()
                }).ToList();

            var vehicleGrouped = (from v in vehicles
                group v by new { v.CreatedAt.Year, v.CreatedAt.Month }
                into g
                select new
                {
                    g.Key.Year,
                    g.Key.Month,
                    NewVehicleCount = g.Count()
                }).ToList();

            var orderGrouped = (from o in orders
                group o by new { o.CreatedAt.Year, o.CreatedAt.Month }
                into g
                select new
                {
                    g.Key.Year,
                    g.Key.Month,
                    NewOrderCount = g.Count()
                }).ToList();

            var allMonths = grouped.Select(x => new { x.Year, x.Month })
                .Union(vehicleGrouped.Select(x => new { x.Year, x.Month }))
                .Union(orderGrouped.Select(x => new { x.Year, x.Month }))
                .Distinct()
                .OrderBy(x => x.Year).ThenBy(x => x.Month)
                .ToList();

            AdminIndexResponses = allMonths.Select(x => new AdminIndexResponse
            {
                Year = x.Year,
                Month = x.Month,
                NewCustomerCount =
                    grouped.FirstOrDefault(g => g.Year == x.Year && g.Month == x.Month)?.NewCustomerCount ??
                    0,
                NewVechicleCount =
                    vehicleGrouped.FirstOrDefault(v => v.Year == x.Year && v.Month == x.Month)?.NewVehicleCount ?? 0,
                NewOrderCount =
                    orderGrouped.FirstOrDefault(o => o.Year == x.Year && o.Month == x.Month)?.NewOrderCount ?? 0,
                NewDealCount = accounts.Count(a => a.CreatedAt.Year == x.Year && a.CreatedAt.Month == x.Month)
            }).ToList();

            for (var i = 1; i < AdminIndexResponses.Count; i++)
            {
                var current = AdminIndexResponses[i];
                var prev = AdminIndexResponses[i - 1];

                current.CustomerGrowth = CalculateGrowth(prev.NewCustomerCount, current.NewCustomerCount);
                current.VehicleGrowth = CalculateGrowth(prev.NewVechicleCount, current.NewVechicleCount);
                current.OrderGrowth = CalculateGrowth(prev.NewOrderCount, current.NewOrderCount);
                current.DealGrowth = CalculateGrowth(prev.NewDealCount, current.NewDealCount);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    private double CalculateGrowth(int prev, int current)
    {
        if (prev == 0 && current == 0) return 0;
        if (prev == 0) return 100; // tăng mạnh nếu tháng trước không có
        return Math.Round((double)(current - prev) / prev * 100, 2);
    }
}