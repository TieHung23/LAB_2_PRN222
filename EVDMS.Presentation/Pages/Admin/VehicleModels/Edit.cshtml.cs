using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using EVDMS.DAL.Repositories.Abstractions; // Đảm bảo bạn đã using cái này

namespace EVDMS.Presentation.Pages.Admin.VehicleModels
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IVehicleConfigService _vehicleConfigService;
        private readonly IVehicleModelRepository _vehicleModelRepository; // Dùng để Validation

        public EditModel(
            IVehicleModelService vehicleModelService,
            IVehicleConfigService vehicleConfigService,
            IVehicleModelRepository vehicleModelRepository)
        {
            _vehicleModelService = vehicleModelService;
            _vehicleConfigService = vehicleConfigService;
            _vehicleModelRepository = vehicleModelRepository;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        // InputModel này giống hệt Create, nhưng thêm 2 ID
        public class InputModel
        {
            // ID của Model và Config
            public Guid Id { get; set; }
            public Guid VehicleConfigId { get; set; }

            // --- Thuộc tính của VehicleModel ---
            [Required(ErrorMessage = "Vui lòng nhập tên mẫu xe.")]
            [Display(Name = "Tên Mẫu xe")]
            public string ModelName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Vui lòng nhập hãng xe.")]
            [Display(Name = "Hãng xe")]
            public string Brand { get; set; } = string.Empty;

            [Required(ErrorMessage = "Vui lòng nhập loại xe.")]
            [Display(Name = "Loại xe")]
            public string VehicleType { get; set; } = string.Empty;

            [Display(Name = "Mô tả")]
            public string Description { get; set; } = string.Empty;

            [Display(Name = "Đường dẫn ảnh (ImgUrl)")]
            [Url(ErrorMessage = "Đường dẫn ảnh không hợp lệ.")]
            public string ImgUrl { get; set; } = string.Empty;

            [Required(ErrorMessage = "Vui lòng nhập năm ra mắt.")]
            [Range(1900, 2100, ErrorMessage = "Năm ra mắt không hợp lệ.")]
            [Display(Name = "Năm ra mắt")]
            public int ReleaseYear { get; set; } = DateTime.Now.Year;

            [Display(Name = "Đang hoạt động?")]
            public bool IsActive { get; set; } = true;

            // --- Thuộc tính của VehicleConfig ---
            [Required(ErrorMessage = "Vui lòng nhập tên phiên bản.")]
            [Display(Name = "Tên Phiên bản (ví dụ: Standard, Plus...)")]
            public string VersionName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Vui lòng nhập màu sắc.")]
            [Display(Name = "Màu sắc")]
            public string Color { get; set; } = string.Empty;

            [Required(ErrorMessage = "Vui lòng nhập loại nội thất.")]
            [Display(Name = "Loại nội thất")]
            public string InteriorType { get; set; } = string.Empty;

            [Required(ErrorMessage = "Vui lòng nhập giá cơ bản.")]
            [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
            [Display(Name = "Giá cơ bản")]
            public decimal BasePrice { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập thời gian bảo hành.")]
            [Range(0, 240, ErrorMessage = "Thời gian bảo hành không hợp lệ (0-240 tháng).")]
            [Display(Name = "Thời gian bảo hành (tháng)")]
            public int WarrantyPeriod { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Dùng phương thức GetModelWithConfigByIdAsync để lấy cả 2
            var vehicleModel = await _vehicleModelService.GetModelWithConfigByIdAsync(id.Value);

            if (vehicleModel == null || vehicleModel.VehicleConfig == null)
            {
                // Đây có thể là nơi gây ra lỗi 404
                return NotFound("Không tìm thấy mẫu xe hoặc cấu hình xe đi kèm.");
            }

            // Map dữ liệu từ 2 đối tượng vào 1 InputModel
            Input = new InputModel
            {
                // Model data
                Id = vehicleModel.Id,
                ModelName = vehicleModel.ModelName,
                Brand = vehicleModel.Brand,
                VehicleType = vehicleModel.VehicleType,
                Description = vehicleModel.Description,
                ImgUrl = vehicleModel.ImgUrl,
                ReleaseYear = vehicleModel.ReleaseYear,
                IsActive = vehicleModel.IsActive,

                // Config data
                VehicleConfigId = vehicleModel.VehicleConfig.Id,
                VersionName = vehicleModel.VehicleConfig.VersionName,
                Color = vehicleModel.VehicleConfig.Color,
                InteriorType = vehicleModel.VehicleConfig.InteriorType,
                BasePrice = vehicleModel.VehicleConfig.BasePrice,
                WarrantyPeriod = vehicleModel.VehicleConfig.WarrantyPeriod
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Hiển thị lại form với lỗi
            }

            // Lấy ID của Admin đang đăng nhập
            var updatedByIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(updatedByIdString) || !Guid.TryParse(updatedByIdString, out var updatedById))
            {
                ModelState.AddModelError(string.Empty, "Không thể xác thực người dùng.");
                return Page();
            }

            try
            {
                // 1. Kiểm tra logic validation (Model + Version)
                // Phải kiểm tra xem tên này đã tồn tại ở 1 ID khác (Id != Input.Id) hay chưa
                var existingCombination = await _vehicleModelRepository.GetByModelAndVersionAsync(Input.ModelName, Input.VersionName);
                if (existingCombination != null && existingCombination.Id != Input.Id)
                {
                    ModelState.AddModelError(string.Empty, $"Sự kết hợp Mẫu xe '{Input.ModelName}' và Phiên bản '{Input.VersionName}' đã tồn tại cho một xe khác.");
                    return Page();
                }

                // 2. Lấy các đối tượng gốc từ DB
                var modelToUpdate = await _vehicleModelService.GetModelWithConfigByIdAsync(Input.Id);
                if (modelToUpdate == null || modelToUpdate.VehicleConfig == null)
                {
                    return NotFound("Không tìm thấy đối tượng gốc để cập nhật.");
                }

                var configToUpdate = modelToUpdate.VehicleConfig;

                // 3. Cập nhật đối tượng VehicleModel
                modelToUpdate.ModelName = Input.ModelName;
                modelToUpdate.Brand = Input.Brand;
                modelToUpdate.VehicleType = Input.VehicleType;
                modelToUpdate.Description = Input.Description;
                modelToUpdate.ImgUrl = Input.ImgUrl;
                modelToUpdate.ReleaseYear = Input.ReleaseYear;
                modelToUpdate.IsActive = Input.IsActive;

                await _vehicleModelService.UpdateVehicleModelAsync(modelToUpdate); // Dùng hàm update đã có

                // 4. Cập nhật đối tượng VehicleConfig
                configToUpdate.VersionName = Input.VersionName;
                configToUpdate.Color = Input.Color;
                configToUpdate.InteriorType = Input.InteriorType;
                configToUpdate.BasePrice = Input.BasePrice;
                configToUpdate.WarrantyPeriod = Input.WarrantyPeriod;
                configToUpdate.UpdatedById = updatedById;
                configToUpdate.UpdatedAt = DateTime.UtcNow;
                configToUpdate.UpdatedAtTick = DateTime.UtcNow.Ticks;

                await _vehicleConfigService.UpdateVehicleConfigAsync(configToUpdate); // Dùng hàm update đã có

                TempData["SuccessMessage"] = "Cập nhật mẫu xe thành công!";
                return RedirectToPage("../VehicleModels"); // Chuyển về trang danh sách
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Lỗi khi cập nhật mẫu xe: {ex.Message}");
                return Page();
            }
        }
    }
}