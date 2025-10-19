using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace EVDMS.Presentation.Pages.Dealer
{
    [Authorize(Roles = "Dealer Staff, Dealer Manager")]
    public class VehiclesModel : PageModel
    {
        private readonly IInventoryService _inventoryService;

        public VehiclesModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public IEnumerable<Inventory> Vehicles { get; set; } = new List<Inventory>();

        public async Task OnGetAsync()
        {
            // Lấy DealerId của nhân viên đang đăng nhập từ Claims
            var dealerIdClaim = User.FindFirstValue("DealerId");

            if (Guid.TryParse(dealerIdClaim, out Guid dealerId))
            {
                Vehicles = await _inventoryService.GetByDealerIdAsync(dealerId);
            }
            // Bạn có thể thêm else để xử lý lỗi nếu không tìm thấy DealerId
        }
    }
}