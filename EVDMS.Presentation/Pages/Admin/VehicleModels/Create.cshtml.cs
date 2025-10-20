using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using EVDMS.DAL.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims; // Để lấy User ID

namespace EVDMS.Presentation.Pages.Admin.VehicleModels
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly IVehicleModelService _vehicleModelService;
        private readonly IVehicleConfigService _vehicleConfigService;
        private readonly IVehicleModelRepository _vehicleModelRepository;
        public CreateModel(
    IVehicleModelService vehicleModelService,
    IVehicleConfigService vehicleConfigService,
    IVehicleModelRepository vehicleModelRepository)
        {
            _vehicleModelService = vehicleModelService;
            _vehicleConfigService = vehicleConfigService;
            _vehicleModelRepository = vehicleModelRepository;
        }

        // Tạo một ViewModel để chứa cả Model và Config
        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public class InputModel
        {
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

        public void OnGet()
        {
            // Chỉ cần hiển thị trang, không cần logic gì khi tải
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Hiển thị lại form với lỗi
            }

            // Lấy ID của Admin đang đăng nhập
            var createdByIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(createdByIdString) || !Guid.TryParse(createdByIdString, out var createdById))
            {
                ModelState.AddModelError(string.Empty, "Không thể xác thực người dùng.");
                return Page();
            }

            try
            {
                var existingCombination = await _vehicleModelRepository.GetByModelAndVersionAsync(Input.ModelName, Input.VersionName);
                if (existingCombination != null)
                {
                    ModelState.AddModelError(string.Empty, $"Sự kết hợp Mẫu xe '{Input.ModelName}' và Phiên bản '{Input.VersionName}' đã tồn tại.");
                    return Page();
                }
                // 1. Tạo VehicleConfig TRƯỚC
                var newVehicleConfig = new VehicleConfig
                {
                    VersionName = Input.VersionName,
                    Color = Input.Color,
                    InteriorType = Input.InteriorType,
                    BasePrice = Input.BasePrice,
                    WarrantyPeriod = Input.WarrantyPeriod,
                    IsDeleted = false,
                    // Gán thông tin CreatedBy...
                    CreatedById = createdById,
                    CreatedAt = DateTime.UtcNow,
                    CreatedAtTick = DateTime.UtcNow.Ticks
                };

                var createdConfig = await _vehicleConfigService.AddAndReturnAsync(newVehicleConfig);

                if (createdConfig == null)
                {
                    ModelState.AddModelError(string.Empty, "Lỗi khi tạo cấu hình xe (Config).");
                    return Page();
                }

                // 2. Tạo đối tượng VehicleModel, liên kết với Config vừa tạo
                var newModel = new VehicleModel
                {
                    ModelName = Input.ModelName,
                    Brand = Input.Brand,
                    VehicleType = Input.VehicleType,
                    Description = Input.Description,
                    ImgUrl = Input.ImgUrl,
                    ReleaseYear = Input.ReleaseYear,
                    IsActive = Input.IsActive,
                    IsDeleted = false, // Mặc định khi tạo
                    VehicleConfigId = createdConfig.Id, // Liên kết ID
                    // Gán thông tin CreatedBy...
                    CreatedById = createdById,
                    CreatedAt = DateTime.UtcNow,
                    CreatedAtTick = DateTime.UtcNow.Ticks
                    // Chúng ta chưa xử lý Features ở đây
                };

                // 3. Lưu Model vào DB (Sử dụng CreateAsync từ service)
                await _vehicleModelService.CreateAsync(newModel);

                TempData["SuccessMessage"] = "Thêm mẫu xe mới thành công!";
                return RedirectToPage("../VehicleModels"); // Chuyển về trang danh sách
            }
            catch (Exception ex)
            {
                // Log lỗi
                ModelState.AddModelError(string.Empty, $"Lỗi khi thêm mẫu xe: {ex.Message}");
                return Page();
            }
        }
    }
}