using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class FeaturesModel : PageModel
    {
        private readonly IFeatureService _featureService;

        public FeaturesModel( IFeatureService featureService )
        {
            _featureService = featureService;
        }

        public IEnumerable<Feature> Features
        {
            get; set;
        }

        [BindProperty]
        public Feature Feature
        {
            get; set;
        }

        public async Task OnGetAsync()
        {
            Features = await _featureService.GetAllFeatureAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Placeholder:
            // await _vehicleModelService.SaveFeatureAsync(Feature);
            return RedirectToPage();
        }
    }
}