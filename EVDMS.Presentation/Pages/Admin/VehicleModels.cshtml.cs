using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class VehicleModelsModel : PageModel
    {
        private readonly IVehicleModelService _vehicleModelService;

        public VehicleModelsModel(IVehicleModelService vehicleModelService)
        {
            _vehicleModelService = vehicleModelService;
        }

        // ✅ SỬA LỖI: Khởi tạo danh sách để không bao giờ bị null
        public IEnumerable<VehicleModel> VehicleModels { get; set; } = new List<VehicleModel>();

        [BindProperty]
        public VehicleModel VmModel { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        [TempData]
        public string SuccessMessage { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                VehicleModels = await _vehicleModelService.SearchAsync(SearchTerm);
            }
            else
            {
                VehicleModels = await _vehicleModelService.GetAllAsync();
            }
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                ErrorMessage = "Dữ liệu không hợp lệ: " + string.Join("; ", errors);
                // Giữ lại trang với dữ liệu đã nhập để người dùng sửa
                VehicleModels = await _vehicleModelService.GetAllAsync();
                return Page();
            }

            try
            {
                await _vehicleModelService.CreateAsync(VmModel);
                SuccessMessage = $"Thêm mẫu xe '{VmModel.ModelName}' thành công!";
            }
            catch (System.Exception ex)
            {
                ErrorMessage = "Lỗi khi thêm mới: " + ex.Message;
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnGetModelJsonAsync(Guid id)
        {
            var model = await _vehicleModelService.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return new JsonResult(model);
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                ErrorMessage = "Dữ liệu không hợp lệ: " + string.Join("; ", errors);
                return RedirectToPage();
            }

            try
            {
                await _vehicleModelService.UpdateAsync(VmModel.Id, VmModel);
                SuccessMessage = $"Cập nhật mẫu xe '{VmModel.ModelName}' thành công!";
            }
            catch (System.Exception ex)
            {
                ErrorMessage = "Lỗi khi cập nhật: " + ex.Message;
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            try
            {
                await _vehicleModelService.DeleteAsync(id);
                SuccessMessage = "Xóa mẫu xe thành công!";
            }
            catch (System.Exception ex)
            {
                ErrorMessage = "Lỗi khi xóa: " + ex.Message;
            }
            return RedirectToPage();
        }
    }
}