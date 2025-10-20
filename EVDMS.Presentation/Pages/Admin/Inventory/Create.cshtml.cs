using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.DTOs;
using EVDMS.Presentation.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;

namespace EVDMS.Presentation.Pages.Admin.Inventory
{
    public class CreateModel : PageModel
    {
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IDealerService _dealerService;
        private readonly IInventoryService _inventorySerivce;
        private readonly IHubContext<NotificationHub> _hub;

        // CHỈ GIỮ LẠI CONSTRUCTOR NÀY
        public CreateModel( IVehicleModelService vehicleModelService, IDealerService dealerService, IInventoryService inventorySerivce, IHubContext<NotificationHub> hub )
        {
            _vehicleModelService = vehicleModelService;
            _dealerService = dealerService;
            _inventorySerivce = inventorySerivce;
            _hub = hub;
        }

        [BindProperty]
        public CreateInventory Input
        {
            get; set;
        }

        public SelectList VehicleConfigs
        {
            get; set;
        }
        public SelectList Dealers
        {
            get; set;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadDropdownsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if( !ModelState.IsValid )
            {
                await LoadDropdownsAsync();
                return Page();
            }

            var result = await _inventorySerivce.CreateMultipleInventory( Input );



            if( result == true )
            {
                TempData["SuccessMessage"] = "New stock record created successfully!";

                string targetDealerId = Input.DealerId.ToString();

                await _hub.Clients.Group( targetDealerId ).SendAsync(
                    "AssignVehicleSuccess",
                    $"Admin has assigned {Input.StockQuantity} new car(s) to your dealership."
                );
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to create new stock record. Please try again.";
            }
            return RedirectToPage( "/Admin/Inventory" );
        }

        private async Task LoadDropdownsAsync()
        {
            // Vấn đề nhỏ: Bạn đang lấy VehicleModel, nhưng property là VehicleConfigs.
            // Đoạn code này cần lấy VehicleConfiguration thay vì VehicleModel.
            // Tôi sẽ tạm sửa lại tên service để code dễ hiểu hơn.
            // Giả sử bạn có một IVehicleConfigService.
            var vehicleConfigsData = ( await _vehicleModelService.GetAllAsync() ).Select( x => new
            {
                Id = x.Id,
                Name = x.ModelName // Cần hiển thị cả màu sắc, phiên bản nếu có
            } );
            VehicleConfigs = new SelectList( vehicleConfigsData, "Id", "Name" );

            var dealersData = ( await _dealerService.GetAllAsync() ).Select( x => new
            {
                Id = x.Id,
                Name = x.Name
            } );
            Dealers = new SelectList( dealersData, "Id", "Name" );
        }
    }
}