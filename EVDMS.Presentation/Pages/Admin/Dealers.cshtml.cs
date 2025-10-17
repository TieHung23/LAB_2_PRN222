using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class DealersModel : PageModel
    {
        private readonly IDealerService _dealerService;
        // Inject IAccountService để lấy danh sách manager

        public DealersModel(IDealerService dealerService)
        {
            _dealerService = dealerService;
        }

        public IEnumerable<Dealer> Dealers { get; set; }

        [BindProperty]
        public Dealer Dealer { get; set; }

        public async Task OnGetAsync()
        {
            // Placeholder:
            // Dealers = await _dealerService.GetAllAsync();
            Dealers = new List<Dealer>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Placeholder:
            // await _dealerService.SaveAsync(Dealer);
            return RedirectToPage();
        }
    }
}
