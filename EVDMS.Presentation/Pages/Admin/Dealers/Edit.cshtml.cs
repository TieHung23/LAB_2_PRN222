using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // Cần cho DbUpdateConcurrencyException

namespace EVDMS.Presentation.Pages.Admin.Dealers
{
    public class EditModel : PageModel
    {
        private readonly IDealerService _dealerService;

        public EditModel(IDealerService dealerService)
        {
            _dealerService = dealerService;
        }

        [BindProperty]
        public Dealer Dealer { get; set; } = new();

        // Hàm OnGet để tải dữ liệu dealer cần sửa
        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Gọi service để lấy dealer theo ID
            Dealer = await _dealerService.GetDealerByIdAsync(id.Value);

            if (Dealer == null)
            {
                return NotFound(); // Không tìm thấy dealer
            }
            return Page();
        }

        // Hàm OnPost để xử lý cập nhật
        public async Task<IActionResult> OnPostAsync()
        {
            // --- VALIDATION TRÙNG LẶP (Bỏ qua Code, chỉ kiểm tra Name và Email) ---
            // excludeId được dùng để loại trừ chính dealer đang sửa ra khỏi việc kiểm tra
            if (await _dealerService.NameExistsAsync(Dealer.Name, Dealer.Id))
            {
                ModelState.AddModelError("Dealer.Name", "Dealer Name already exists.");
            }
            if (await _dealerService.EmailExistsAsync(Dealer.Email, Dealer.Id))
            {
                ModelState.AddModelError("Dealer.Email", "Dealer Email already exists.");
            }
            // --- KẾT THÚC VALIDATION ---

            if (!ModelState.IsValid)
            {
                // Cần tải lại Code vì nó readonly và không được bind lại
                var originalDealer = await _dealerService.GetDealerByIdAsync(Dealer.Id);
                if (originalDealer != null) Dealer.Code = originalDealer.Code;
                return Page(); // Trả về form với lỗi
            }

            // --- LOGIC UPDATE ---
            try
            {
                // Lấy entity gốc từ DB để cập nhật (Cách làm an toàn hơn là attach)
                var dealerToUpdate = await _dealerService.GetDealerByIdAsync(Dealer.Id);

                if (dealerToUpdate == null)
                {
                    return NotFound();
                }

                // Cập nhật các trường cho phép thay đổi
                dealerToUpdate.Name = Dealer.Name;
                dealerToUpdate.Address = Dealer.Address;
                dealerToUpdate.PhoneNumber = Dealer.PhoneNumber;
                dealerToUpdate.Email = Dealer.Email;
                dealerToUpdate.Region = Dealer.Region;
                dealerToUpdate.IsActive = Dealer.IsActive;
                // KHÔNG cập nhật Code

                await _dealerService.UpdateDealerAsync(dealerToUpdate);

                TempData["SuccessMessage"] = $"Dealer '{dealerToUpdate.Name}' updated successfully!";
                return RedirectToPage("../Dealers"); // Redirect về danh sách
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError(string.Empty, "The dealer was modified by another user. Please reload and try again.");
                // Tải lại Code
                var originalDealer = await _dealerService.GetDealerByIdAsync(Dealer.Id);
                if (originalDealer != null) Dealer.Code = originalDealer.Code;
                return Page();
            }
            catch (Exception ex)
            {
                // Log lỗi
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                // Tải lại Code
                var originalDealer = await _dealerService.GetDealerByIdAsync(Dealer.Id);
                if (originalDealer != null) Dealer.Code = originalDealer.Code;
                return Page();
            }
        }
    }
}