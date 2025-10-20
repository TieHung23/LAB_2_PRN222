using EVDMS.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EVDMS.Presentation.Pages.Admin
{
    public class InventoryModel : PageModel
    {
        private readonly IInventoryService _inventoryService;

        public InventoryModel( IInventoryService inventoryService )
        {
            _inventoryService = inventoryService;
        }

        public List<InventoryViewModel> Inventories { get; set; } = new();

        // Properties for dropdown lists in the modal
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
            var result = await _inventoryService.GetAllInventoryAsync();

            Inventories = result
                .Select( i => new InventoryViewModel
                {
                    Id = i.Id,
                    VehicleName = i.VehicleModel.ModelName,
                    VehicleModel = i.VehicleModel.VehicleType,
                    Color = i.VehicleModel.VehicleConfig.Color,
                    DealerName = i.Dealer != null ? i.Dealer.Name : null
                } ).ToList();

            return Page();
        }
        public class InventoryViewModel
        {
            public Guid Id
            {
                get; set;
            }
            public string VehicleName
            {
                get; set;
            }
            public string VehicleModel
            {
                get; set;
            }
            public string Color
            {
                get; set;
            }
            public string? DealerName
            {
                get; set;
            } // Nullable for Central Warehouse
        }
    }
}