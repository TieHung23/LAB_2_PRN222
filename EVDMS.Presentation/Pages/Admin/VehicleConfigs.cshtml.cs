using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class VehicleConfigsModel : PageModel
    {
        private readonly IVehicleConfigService _vehicleConfigService;

        public VehicleConfigsModel(IVehicleConfigService vehicleConfigService)
        {
            _vehicleConfigService = vehicleConfigService;
        }

        public IEnumerable<VehicleConfig> VehicleConfigs { get; set; } = new List<VehicleConfig>();

        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            VehicleConfigs = await _vehicleConfigService.GetAllAsync();
        }
    }
}