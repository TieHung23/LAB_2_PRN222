using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using AccountEntity = EVDMS.Core.Entities.Account;

namespace EVDMS.Presentation.Pages.Dealer
{
    [Authorize(Roles = "Dealer Staff, Dealer Manager")]
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService;

        // --- Dữ liệu CÁ NHÂN (cho cả Staff và Manager) ---
        public decimal StaffTotalRevenue { get; set; }
        public int StaffTotalOrders { get; set; }
        public IEnumerable<Order> RecentStaffOrders { get; set; } = new List<Order>();
        public string StaffFullName { get; set; }

        // --- Dữ liệu TOÀN CHI NHÁNH (CHỈ DÀNH CHO MANAGER) ---
        public decimal DealerTotalRevenue { get; set; }
        public int DealerTotalOrders { get; set; }
        public IEnumerable<Order> RecentDealerOrders { get; set; } = new List<Order>();

        // Danh sách doanh thu của từng nhân viên, chỉ dành cho Manager
        public List<(AccountEntity Staff, decimal Revenue)> StaffRevenues { get; set; } = new List<(AccountEntity, decimal Revenue)>();

        public IndexModel(IOrderService orderService, IAccountService accountService)
        {
            _orderService = orderService;
            _accountService = accountService;
            StaffFullName = "Staff"; // Giá trị mặc định
        }

        public async Task OnGetAsync()
        {
            // --- PHẦN 1: TẢI DỮ LIỆU CÁ NHÂN (cho tất cả mọi người) ---
            var staffAccountIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            StaffFullName = User.FindFirstValue(ClaimTypes.Name) ?? "Staff";

            if (Guid.TryParse(staffAccountIdClaim, out Guid staffAccountId))
            {
                // Gọi các hàm service cho chính nhân viên này
                StaffTotalRevenue = await _orderService.GetTotalRevenueByStaffIdAsync(staffAccountId);
                var staffOrders = await _orderService.GetOrdersByStaffIdAsync(staffAccountId);

                RecentStaffOrders = staffOrders.Take(10); // Chỉ hiển thị 10 đơn hàng gần nhất
                StaffTotalOrders = staffOrders.Count();
            }

            // --- PHẦN 2: TẢI THÊM DỮ LIỆU NẾU LÀ MANAGER ---
            if (User.IsInRole("Dealer Manager"))
            {
                var dealerIdClaim = User.FindFirstValue("DealerId"); // Lấy DealerId từ Claims

                if (Guid.TryParse(dealerIdClaim, out Guid dealerId))
                {
                    // Lấy tổng doanh thu toàn chi nhánh
                    DealerTotalRevenue = await _orderService.GetTotalRevenueByDealerIdAsync(dealerId);

                    // Lấy tổng đơn hàng toàn chi nhánh
                    var dealerOrders = await _orderService.GetOrdersByDealerIdAsync(dealerId);
                    DealerTotalOrders = dealerOrders.Count();

                    // Lấy 10 đơn hàng gần nhất của toàn chi nhánh (bao gồm cả nhân viên khác)
                    RecentDealerOrders = dealerOrders.OrderByDescending(o => o.CreatedAt).Take(10);

                    // Lấy doanh thu theo từng nhân viên
                    StaffRevenues = await _orderService.GetStaffRevenuesByDealerAsync(dealerId);
                }
            }
        }
    }
}