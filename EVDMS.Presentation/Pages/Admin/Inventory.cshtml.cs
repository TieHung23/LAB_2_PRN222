using EVDMS.BLL.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EVDMS.Presentation.Pages.Admin
{
    public class InventoryModel : PageModel
    {
        private readonly IInventoryService _inventoryService;
        // Inject IVehicleModelService và IDealerService để lấy dropdowns

        public InventoryModel( IInventoryService inventoryService )
        {
            _inventoryService = inventoryService;
        }

        public List<InventoryViewModel> Inventories { get; set; } = new();

        // [BindProperty] is used to bind the form data from the modal on POST
        [BindProperty]
        public InventoryInputModel Inventory
        {
            get; set;
        }

        // Properties for dropdown lists in the modal
        public SelectList VehicleConfigs
        {
            get; set;
        }
        public SelectList Dealers
        {
            get; set;
        }


        // --- Page Handlers ---

        /// <summary>
        /// GET: This handler is called when the page is first loaded.
        /// It fetches all necessary data to display the inventory list and populate dropdowns.
        /// </summary>
        public async Task<IActionResult> OnGetAsync()
        {
            // Note: Replace the following dummy data with your actual database queries
            await LoadInitialData();

            // Dummy inventory list for demonstration
            Inventories = new List<InventoryViewModel>
            {
                new() { Id = 1, VehicleName = "VF 8", VehicleModel = "Eco", Color = "Red", StockQuantity = 50, DealerName = "Thuduc Dealer" },
                new() { Id = 2, VehicleName = "VF 9", VehicleModel = "Plus", Color = "Black", StockQuantity = 25, DealerName = "Binh Duong Dealer" },
                new() { Id = 3, VehicleName = "VF e34", VehicleModel = "Standard", Color = "White", StockQuantity = 120, DealerName = null } // Central Warehouse
            };

            /*
            // --- REAL DATABASE LOGIC EXAMPLE ---
            Inventories = await _context.Inventories
                .Include(i => i.VehicleConfiguration).ThenInclude(vc => vc.Vehicle)
                .Include(i => i.VehicleConfiguration).ThenInclude(vc => vc.Color)
                .Include(i => i.Dealer)
                .Select(i => new InventoryViewModel
                {
                    Id = i.Id,
                    VehicleName = i.VehicleConfiguration.Vehicle.Name,
                    VehicleModel = i.VehicleConfiguration.Vehicle.Model,
                    Color = i.VehicleConfiguration.Color.Name,
                    StockQuantity = i.StockQuantity,
                    DealerName = i.Dealer != null ? i.Dealer.Name : null
                })
                .ToListAsync();
            */

            return Page();
        }

        /// <summary>
        /// POST: This handler is called when the modal form is submitted.
        /// It handles both creating a new inventory record and updating an existing one.
        /// </summary>
        public async Task<IActionResult> OnPostAsync()
        {
            if( !ModelState.IsValid )
            {
                // If validation fails, reload the page with the data to show validation errors.
                await LoadInitialData();
                return Page();
            }

            if( Inventory.Id == 0 ) // Create new
            {
                // TODO: Add logic to create a new inventory record in the database
                // var newInventory = new Inventory { ... };
                // _context.Inventories.Add(newInventory);
            }
            else // Update existing
            {
                // TODO: Add logic to find and update the existing inventory record
                // var existingInventory = await _context.Inventories.FindAsync(Inventory.Id);
                // if (existingInventory != null) { ... }
            }

            // await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        /// <summary>
        /// GET (AJAX): This handler is called by JavaScript when the "Edit" button is clicked.
        /// It fetches a single inventory item by ID and returns it as JSON.
        /// </summary>
        public async Task<IActionResult> OnGetInventoryForEditAsync( int id )
        {
            // TODO: Replace with your actual database query to find the inventory record
            // var inventory = await _context.Inventories.FindAsync(id);
            // if (inventory == null) { return NotFound(); }
            // return new JsonResult(inventory);

            // Dummy data for demonstration
            var dummyInventory = new
            {
                id = 2,
                vehicleConfigurationId = 2,
                dealerId = 2,
                stockQuantity = 25
            };
            return new JsonResult( dummyInventory );
        }

        // --- Helper Methods ---

        private async Task LoadInitialData()
        {
            // This method loads data needed for the dropdowns in the modal.
            // TODO: Replace with your actual database queries.

            // Dummy data for Vehicle Configurations dropdown
            var vehicleConfigsData = new[]
            {
                new { Id = 1, Name = "VF 8 Eco - Red" },
                new { Id = 2, Name = "VF 9 Plus - Black" },
                new { Id = 3, Name = "VF e34 - White" }
            };
            VehicleConfigs = new SelectList( vehicleConfigsData, "Id", "Name" );

            // Dummy data for Dealers dropdown
            var dealersData = new[]
            {
                new { Id = 1, Name = "Thuduc Dealer" },
                new { Id = 2, Name = "Binh Duong Dealer" }
            };
            Dealers = new SelectList( dealersData, "Id", "Name" );

            /*
            // --- REAL DATABASE LOGIC EXAMPLE ---
             var vehicleConfigsFromDb = await _context.VehicleConfigurations
                .Include(vc => vc.Vehicle)
                .Include(vc => vc.Color)
                .Select(vc => new {
                    Id = vc.Id,
                    DisplayText = $"{vc.Vehicle.Name} {vc.Vehicle.Model} - {vc.Color.Name}"
                }).ToListAsync();
            VehicleConfigs = new SelectList(vehicleConfigsFromDb, "Id", "DisplayText");

            var dealersFromDb = await _context.Dealers
                .Select(d => new { Id = d.Id, d.Name }).ToListAsync();
            Dealers = new SelectList(dealersFromDb, "Id", "Name");
            */
        }
    }

    // --- ViewModels and InputModels ---

    /// <summary>
    /// Represents the data displayed in each row of the inventory table.
    /// </summary>
    public class InventoryViewModel
    {
        public int Id
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
        public int StockQuantity
        {
            get; set;
        }
        public string? DealerName
        {
            get; set;
        } // Nullable for Central Warehouse
    }

    /// <summary>
    /// Represents the data bound from the Add/Edit modal form.
    /// </summary>
    public class InventoryInputModel
    {
        public int Id
        {
            get; set;
        } // 0 for new, >0 for existing

        // Use validation attributes here
        // [Required]
        public int VehicleConfigurationId
        {
            get; set;
        }

        public int? DealerId
        {
            get; set;
        } // Nullable for Central Warehouse

        // [Range(0, int.MaxValue)]
        public int StockQuantity
        {
            get; set;
        }
    }
}