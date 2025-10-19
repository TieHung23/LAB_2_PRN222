using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations; 

namespace EVDMS.Presentation.Pages.Admin.Dealers
{
    public class CreateModel : PageModel
    {
        private readonly IDealerService _dealerService;

        public CreateModel(IDealerService dealerService)
        {
            _dealerService = dealerService;
        }

        [BindProperty]
        public EVDMS.Core.Entities.Dealer Dealer { get; set; }

        public IActionResult OnGet()
        {
            Dealer.IsActive = true;
            return Page();
        }

        // Hàm OnPost để xử lý việc tạo mới
        public async Task<IActionResult> OnPostAsync()
        {
            // --- VALIDATION TRÙNG LẶP ---
            if (await _dealerService.CodeExistsAsync(Dealer.Code))
            {
                ModelState.AddModelError("Dealer.Code", "Dealer Code already exists.");
            }
            if (await _dealerService.NameExistsAsync(Dealer.Name))
            {
                ModelState.AddModelError("Dealer.Name", "Dealer Name already exists.");
            }
            if (await _dealerService.EmailExistsAsync(Dealer.Email))
            {
                ModelState.AddModelError("Dealer.Email", "Dealer Email already exists.");
            }
            // --- KẾT THÚC VALIDATION TRÙNG LẶP ---

            if (!ModelState.IsValid)
            {
                return Page(); 
            }

            try
            {
                // Gọi service để tạo Dealer
                await _dealerService.CreateDealerAsync(Dealer);

                // Thêm thông báo thành công vào TempData
                TempData["SuccessMessage"] = $"Dealer '{Dealer.Name}' created successfully!";

                // Redirect về trang danh sách Dealers
                return RedirectToPage("../Dealers");
            }
            catch (Exception ex)
            {
                // Log lỗi
                ModelState.AddModelError(string.Empty, $"An error occurred while creating the dealer: {ex.Message}");
                return Page(); // Trả về trang với thông báo lỗi chung
            }
        }
    }
}