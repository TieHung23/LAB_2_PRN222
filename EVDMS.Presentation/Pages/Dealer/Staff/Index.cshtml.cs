using EVDMS.Core.Entities;
using EVDMS.DAL;
using EVDMS.DAL.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EVDMS.Presentation.Pages.Dealer.Staff
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<EVDMS.Core.Entities.Account> StaffList { get; set; } = new List<EVDMS.Core.Entities.Account>();

        public async Task OnGetAsync()
        {
            var dealerIdClaim = User.FindFirst("DealerId")?.Value;

            if (dealerIdClaim == null) return;

            var dealerId = Guid.Parse(dealerIdClaim);

            StaffList = await _context.Accounts
                .Include(a => a.Role)
                .Where(a => a.DealerId == dealerId && a.Role.Name == "Dealer Staff")
                .ToListAsync();
        }
    }
}
