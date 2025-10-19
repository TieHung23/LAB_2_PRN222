using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using EVDMS.DAL.Repositories.Abstractions; // Cần dùng để lấy danh sách Models
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering; // Cần cho SelectList

namespace EVDMS.Presentation.Pages.Admin
{
    public class VehicleConfigsModel : PageModel
    {
        private readonly IVehicleConfigService _configService;
        private readonly IVehicleModelRepository _modelRepository; // Inject để lấy danh sách models

        public VehicleConfigsModel(IVehicleConfigService configService, IVehicleModelRepository modelRepository)
        {
            _configService = configService;
            _modelRepository = modelRepository;
            Configs = new List<VehicleConfig>();
            VehicleConfig = new VehicleConfig();
        }


        // Danh sách để hiển thị lên bảng
        public IEnumerable<VehicleConfig> Configs { get; set; }

        // Dùng để binding dữ liệu từ form Add/Edit
        [BindProperty]
        public VehicleConfig VehicleConfig { get; set; }

        // Dùng để binding VehicleModelId được chọn từ dropdown khi tạo mới
        [BindProperty]
        public Guid SelectedVehicleModelId { get; set; }

        // Dùng để đổ dữ liệu vào dropdown chọn Model
        public SelectList VehicleModels { get; set; }

        public async Task OnGetAsync()
        {
            Configs = await _configService.GetAllAsync();

            var models = await _modelRepository.GetAllAsync();
            VehicleModels = new SelectList(models, "Id", "ModelName");
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            ModelState.Remove("VehicleConfig.VehicleModel");
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            try
            {
                await _configService.CreateAndAssignAsync(VehicleConfig, SelectedVehicleModelId);
                TempData["success"] = $"Cấu hình '{VehicleConfig.VersionName}' đã được tạo và gán thành công.";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return RedirectToPage();
        }

        // Handler để cập nhật một config
        public async Task<IActionResult> OnPostUpdateAsync()
        {
            ModelState.Remove("VehicleConfig.VehicleModel");
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            try
            {
                await _configService.UpdateAsync(VehicleConfig.Id, VehicleConfig);
                TempData["success"] = $"Cấu hình '{VehicleConfig.VersionName}' đã được cập nhật thành công.";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            try
            {
                await _configService.DeleteAsync(id);
                TempData["success"] = "Cấu hình đã được xóa thành công.";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return RedirectToPage();
        }

        public async Task<JsonResult> OnGetVehicleConfigAsync(Guid id)
        {
            var config = await _configService.GetByIdAsync(id);
            return new JsonResult(config);
        }
    }
}