using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace EVDMS.Presentation.Pages.Dealer
{
    [Authorize(Roles = "Dealer Staff, Dealer Manager")]
    public class CreateTestDriveModel : PageModel
    {
        private readonly ITestDriveService _testDriveService;
        private readonly ICustomerService _customerService;
        private readonly IInventoryService _inventoryService;

        public CreateTestDriveModel(ITestDriveService testDriveService, ICustomerService customerService, IInventoryService inventoryService)
        {
            _testDriveService = testDriveService;
            _customerService = customerService;
            _inventoryService = inventoryService;
        }

        [BindProperty]
        public TestDriveInput Input { get; set; } = new TestDriveInput();

        public SelectList CustomerOptions { get; set; } = new SelectList(new List<Customer>());
        public SelectList VehicleModelOptions { get; set; } = new SelectList(new List<VehicleModel>());

        public class TestDriveInput
        {
            [Required(ErrorMessage = "Vui lòng chọn khách hàng")]
            public Guid SelectedCustomerId { get; set; }

            [Required(ErrorMessage = "Vui lòng chọn mẫu xe")]
            public Guid SelectedVehicleModelId { get; set; }

            [Required(ErrorMessage = "Vui lòng chọn ngày giờ")]
            [DataType(DataType.DateTime)]
            public DateTime ScheduledDateTime { get; set; } = DateTime.Now.AddDays(1);
        }

        public async Task OnGetAsync()
        {
            var customers = await _customerService.GetAllAsync();
            CustomerOptions = new SelectList(customers, "Id", "FullName");

            var dealerIdClaim = User.FindFirstValue("DealerId");
            if (Guid.TryParse(dealerIdClaim, out Guid dealerId))
            {
                var inventory = await _inventoryService.GetByDealerIdAsync(dealerId);
                var vehicleModels = inventory.Select(i => i.VehicleModel).DistinctBy(vm => vm.Id);
                VehicleModelOptions = new SelectList(vehicleModels, "Id", "ModelName");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(); // Tải lại dropdowns
                return Page();
            }

            var newTestDrive = new TestDrive
            {
                CustomerId = Input.SelectedCustomerId,
                VehicleModelId = Input.SelectedVehicleModelId,
                ScheduledDateTime = Input.ScheduledDateTime,
                IsActive = true,
                IsDeleted = false,
                IsSuccess = false
            };

            await _testDriveService.CreateAsync(newTestDrive);

            TempData["SuccessMessage"] = "Đặt lịch lái thử thành công!";
            return RedirectToPage("/Dealer/TestDrives");
        }
    }
}