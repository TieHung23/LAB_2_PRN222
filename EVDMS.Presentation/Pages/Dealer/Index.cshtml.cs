using EVDMS.BLL.Services.Abstractions; 
using EVDMS.Core.Entities; 
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims; 

namespace EVDMS.Presentation.Pages.Dealer
{
    [Authorize(Roles = "Dealer Staff, Dealer Manager")] 
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IAccountService _accountService; // Giữ lại nếu bạn cần tên nhân viên

        // Thêm các thuộc tính này
        public decimal StaffTotalRevenue { get; set; }
        public int StaffTotalOrders { get; set; }
        public IEnumerable<Order> RecentStaffOrders { get; set; } = new List<Order>();
        public string StaffFullName { get; set; }

        public IndexModel(IOrderService orderService, IAccountService accountService)
        {
            _orderService = orderService;
            _accountService = accountService;
            StaffFullName = "Staff"; // Giá trị mặc định
        }

        public async Task OnGetAsync()
        {
            // Lấy ID và Tên của nhân viên từ Claims
            var staffAccountIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            StaffFullName = User.FindFirstValue(ClaimTypes.Name) ?? "Staff";

            if (Guid.TryParse(staffAccountIdClaim, out Guid staffAccountId))
            {
                // Gọi các hàm service cho chính nhân viên này
                StaffTotalRevenue = await _orderService.GetTotalRevenueByStaffIdAsync(staffAccountId);
                var orders = await _orderService.GetOrdersByStaffIdAsync(staffAccountId);

                RecentStaffOrders = orders.Take(10); // Chỉ hiển thị 10 đơn hàng gần nhất
                StaffTotalOrders = orders.Count();
            }
        }
    }
}