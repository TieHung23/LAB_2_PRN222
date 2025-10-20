using EVDMS.BLL.Services.Abstractions;
using EVDMS.BLL.SignalR;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace EVDMS.Presentation.Pages.Dealer
{
    [Authorize(Roles = "Dealer Staff, Dealer Manager")]
    public class CreateOrderModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IInventoryService _inventoryService;
        private readonly ICustomerService _customerService;
        private readonly IPromotionService _promotionService;
        private readonly IHubContext<NotificationHub> _hub;
        public CreateOrderModel(IOrderService orderService, IInventoryService inventoryService, ICustomerService customerService, IPromotionService promotionService, IHubContext<NotificationHub> hub)
        {
            _orderService = orderService;
            _inventoryService = inventoryService;
            _customerService = customerService;
            _promotionService = promotionService;
            _hub = hub;
        }

        [BindProperty]
        public Guid InventoryId { get; set; }

        [BindProperty]
        public Guid SelectedCustomerId { get; set; }

        [BindProperty]
        public Guid? SelectedPromotionId { get; set; }

        public Inventory SelectedVehicle { get; set; }
        public SelectList CustomerOptions { get; set; }
        public SelectList PromotionOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid inventoryId)
        {
            SelectedVehicle = await _inventoryService.GetByIdAsync(inventoryId);
            if (SelectedVehicle == null)
            {
                return NotFound();
            }

            InventoryId = inventoryId;

            var customers = await _customerService.GetAllAsync();
            CustomerOptions = new SelectList(customers, "Id", "FullName");

            var promotions = (await _promotionService.GetAllAsync()).Where(p => p.IsActive);
            PromotionOptions = new SelectList(promotions, "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(InventoryId);
                return Page();
            }

            var staffAccountId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var newOrder = new Order
            {
                InventoryId = this.InventoryId,
                CustomerId = this.SelectedCustomerId,
                PromotionId = this.SelectedPromotionId == Guid.Empty ? null : this.SelectedPromotionId,
                AccountId = staffAccountId,
                CreatedById = staffAccountId
            };

            var createdOrder = await _orderService.CreateOrderAsync(newOrder);

            await _hub.Clients.All.SendAsync("SendMessage", $"{createdOrder.Account!.DealerId}", $"{createdOrder.CreatedAt}");

            return RedirectToPage("/Dealer/OrderSuccess", new { orderId = createdOrder.Id });
        }
    }
}