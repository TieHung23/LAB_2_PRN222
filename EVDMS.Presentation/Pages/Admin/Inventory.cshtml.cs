using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class InventoryModel : PageModel
    {
        private readonly IInventoryService _inventoryService;
        // Inject IVehicleModelService và IDealerService để lấy dropdowns

        public InventoryModel(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public IEnumerable<Inventory> Inventories { get; set; }

        [BindProperty]
        public Inventory Inventory { get; set; }

        // public IEnumerable<VehicleConfig> VehicleConfigs { get; set; }
        // public IEnumerable<Dealer> Dealers { get; set; }

        public async Task OnGetAsync()
        {
            // Placeholder:
            // Inventories = await _inventoryService.GetAllAsync();
            // VehicleConfigs = await _vehicleModelService.GetAllConfigsAsync();
            // Dealers = await _dealerService.GetAllAsync();
            Inventories = new List<Inventory>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Placeholder:
            // await _inventoryService.SaveStockAsync(Inventory);
            return RedirectToPage();
        }
    }
}