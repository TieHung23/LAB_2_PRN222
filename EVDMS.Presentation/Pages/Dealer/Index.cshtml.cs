using EVDMS.BLL.Services.Abstractions;
using EVDMS.BLL.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text.Json; 

namespace EVDMS.Presentation.Pages.Dealer
{
    public class StaffDashboardViewModel
    {
        public decimal TotalRevenue { get; set; }
        public int CarsSold { get; set; }
        public int TestDrivesToday { get; set; }
        public int TestDrivesThisWeek { get; set; }

        public string RevenueChartDataJson { get; set; } = "{}";
        public string SalesByTypeChartDataJson { get; set; } = "{}";
    }

    public class ManagerDashboardViewModel
    {
        public decimal BranchTotalRevenue { get; set; }
        public int BranchCarsSold { get; set; }
        public int BranchTestDrivesToday { get; set; }
        public int BranchStaffCount { get; set; }

        // Dữ liệu cho biểu đồ (dưới dạng JSON string)
        public string RevenueChartDataJson { get; set; } = "{}";
        public string SalesByTypeChartDataJson { get; set; } = "{}";
    }

    // --- HẾT VIEWMODEL ---


    [Authorize(Roles = "Dealer Staff, Dealer Manager")]
    public class IndexModel : PageModel
    {
        private readonly IDealerService _dealerService;
        private readonly IOrderService _orderService;
        private readonly ITestDriveService _testDriveService;
        private readonly IAccountService _accountService;

        public EVDMS.Core.Entities.Dealer? CurrentDealer { get; set; }
        public ManagerDashboardViewModel? ManagerData { get; set; }
        public StaffDashboardViewModel? StaffData { get; set; }

        // Constructor giữ nguyên
        public IndexModel(
            IDealerService dealerService,
            IOrderService orderService,
            ITestDriveService testDriveService,
            IAccountService accountService)
        {
            _dealerService = dealerService;
            _orderService = orderService;
            _testDriveService = testDriveService;
            _accountService = accountService;
        }

        // === HÀM ONGETASYNC ĐÃ ĐƯỢC CẬP NHẬT VỚI DỮ LIỆU ẢO CHO BIỂU ĐỒ ===
        public async Task<IActionResult> OnGetAsync()
        {
            var dealerIdClaim = User.FindFirst("DealerId");
            if (dealerIdClaim == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy thông tin đại lý.";
                return RedirectToPage("/Index");
            }
            var dealerId = Guid.Parse(dealerIdClaim.Value);
            CurrentDealer = await _dealerService.GetDealerByIdAsync(dealerId);

            // Dữ liệu nhãn chung
            var chartLabels = new[] { "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10" };
            var carTypeLabels = new[] { "VF 8", "VF 9", "VF e34" };

            // === LOGIC PHÂN CHIA VAI TRÒ (VỚI DỮ LIỆU ẢO) ===
            if (User.IsInRole("Dealer Manager"))
            {
                // ---- DỮ LIỆU ẢO CHO MANAGER ----
                ManagerData = new ManagerDashboardViewModel
                {
                    BranchTotalRevenue = 8750000000, // 8.75 Tỷ
                    BranchCarsSold = 52,
                    BranchTestDrivesToday = 8,
                    BranchStaffCount = 14,

                    // Dữ liệu biểu đồ doanh thu (6 tháng)
                    RevenueChartDataJson = JsonSerializer.Serialize(new
                    {
                        labels = chartLabels,
                        datasets = new[]
                        {
                            new {
                                label = "Doanh thu Chi nhánh",
                                data = new[] { 1200, 1500, 1100, 1800, 1700, 1900 }, // Đơn vị: triệu VNĐ
                                backgroundColor = "rgba(102, 126, 234, 0.6)", // Màu gradient từ layout
                                borderColor = "#667eea",
                                borderWidth = 2,
                                fill = true,
                                tension = 0.4
                            }
                        }
                    }),

                    // Dữ liệu biểu đồ tròn (tỷ lệ xe bán)
                    SalesByTypeChartDataJson = JsonSerializer.Serialize(new
                    {
                        labels = carTypeLabels,
                        datasets = new[]
                        {
                            new {
                                label = "Số lượng xe bán",
                                data = new[] { 25, 15, 12 }, // Tổng 52 xe
                                backgroundColor = new[] { "#667eea", "#764ba2", "#f09819" }
                            }
                        }
                    })
                };
            }
            else // Mặc định là Dealer Staff
            {
                // ---- DỮ LIỆU ẢO CHO STAFF ----
                StaffData = new StaffDashboardViewModel
                {
                    TotalRevenue = 1800000000, // 1.8 Tỷ
                    CarsSold = 12,
                    TestDrivesToday = 2,
                    TestDrivesThisWeek = 7,

                    // Dữ liệu biểu đồ doanh thu cá nhân (6 tháng)
                    RevenueChartDataJson = JsonSerializer.Serialize(new
                    {
                        labels = chartLabels,
                        datasets = new[]
                        {
                            new {
                                label = "Doanh thu Cá nhân",
                                data = new[] { 300, 400, 200, 500, 150, 250 }, // Đơn vị: triệu VNĐ
                                backgroundColor = "rgba(10, 207, 151, 0.6)", // Màu xanh lá
                                borderColor = "#0acf97",
                                borderWidth = 2,
                                fill = true,
                                tension = 0.4
                            }
                        }
                    }),

                    // Dữ liệu biểu đồ tròn (tỷ lệ xe bán)
                    SalesByTypeChartDataJson = JsonSerializer.Serialize(new
                    {
                        labels = carTypeLabels,
                        datasets = new[]
                        {
                            new {
                                label = "Số lượng xe bán",
                                data = new[] { 5, 4, 3 }, // Tổng 12 xe
                                backgroundColor = new[] { "#0acf97", "#00BFFF", "#FFD700" }
                            }
                        }
                    })
                };
            }

            return Page();
        }
    }
}