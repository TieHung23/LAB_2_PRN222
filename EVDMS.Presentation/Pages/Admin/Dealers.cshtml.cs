using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class DealersModel : PageModel
    {
        private readonly IDealerService _dealerService;

        public DealersModel(IDealerService dealerService)
        {
            _dealerService = dealerService;
        }

        // Danh sách để hiển thị
        public IEnumerable<EVDMS.Core.Entities.Dealer> Dealers { get; set; } = new List<EVDMS.Core.Entities.Dealer>();

        // Xóa BindProperty Dealer

        public async Task OnGetAsync()
        {
            // Gọi service để lấy danh sách
            Dealers = await _dealerService.GetAllAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            try
            {
                await _dealerService.DeleteDealerAsync(id);
                TempData["SuccessMessage"] = "Dealer deleted successfully.";
            }
            // --- Bắt lỗi cụ thể ---
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message; // Hiển thị thông báo từ Service
            }
            // --- Bắt các lỗi khác ---
            catch (Exception ex)
            {
               
                TempData["ErrorMessage"] = "An unexpected error occurred while deleting the dealer. Check logs for details.";
                
            }
            return RedirectToPage("./Dealers");
        }
    }
}