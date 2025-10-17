using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class PromotionsModel : PageModel
    {
        private readonly IPromotionService _promotionService;

        public PromotionsModel(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        public IEnumerable<Promotion> Promotions { get; set; }

        [BindProperty]
        public Promotion Promotion { get; set; }

        public async Task OnGetAsync()
        {
            // Placeholder:
            // Promotions = await _promotionService.GetAllAsync();
            Promotions = new List<Promotion>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Placeholder:
            // await _promotionService.SaveAsync(Promotion);
            return RedirectToPage();
        }
    }
}