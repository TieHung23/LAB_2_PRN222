using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class FeaturesModel : PageModel
    {
        private readonly IVehicleModelService _vehicleModelService; // Hoặc một service riêng cho Feature

        public FeaturesModel(IVehicleModelService vehicleModelService)
        {
            _vehicleModelService = vehicleModelService;
        }

        public IEnumerable<Feature> Features { get; set; }

        [BindProperty]
        public Feature Feature { get; set; }

        public async Task OnGetAsync()
        {
            // Placeholder:
            // Features = await _vehicleModelService.GetAllFeaturesAsync();
            Features = new List<Feature>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Placeholder:
            // await _vehicleModelService.SaveFeatureAsync(Feature);
            return RedirectToPage();
        }
    }
}