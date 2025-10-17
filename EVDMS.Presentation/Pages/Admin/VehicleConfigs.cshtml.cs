using EVDMS.Core.Entities; // Đảm bảo bạn đã tham chiếu (using) Entities
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class VehicleConfigsModel : PageModel
    {
        // Danh sách để hiển thị lên giao diện
        public IEnumerable<VehicleConfig> Configs { get; set; }

        // Bạn có thể inject service của mình ở đây khi sẵn sàng
        // private readonly IVehicleConfigService _configService;
        // public VehicleConfigsModel(IVehicleConfigService configService)
        // {
        //     _configService = configService;
        // }

        public VehicleConfigsModel()
        {
            // Khởi tạo danh sách trống
            Configs = new List<VehicleConfig>();
        }

        public void OnGet()
        {
            // Khi bạn có service, bạn sẽ gọi:
            // Configs = _configService.GetAll();

            // Bây giờ, chúng ta sẽ dùng dữ liệu giả để giao diện hiển thị
            // (Dữ liệu này sẽ KHÔNG được hiển thị vì giao diện đang hard-code,
            // nhưng đây là cách làm đúng)
            Configs = new List<VehicleConfig>
            {
                new VehicleConfig { Id = Guid.NewGuid(), VersionName = "VF8 Eco", Color = "Deep Blue", InteriorType = "Beige Vegan Leather", BasePrice = 45000, WarrantyPeriod = 120 },
                new VehicleConfig { Id = Guid.NewGuid(), VersionName = "VF9 Plus", Color = "Crimson Red", InteriorType = "Black Vegan Leather", BasePrice = 62000, WarrantyPeriod = 120 },
                new VehicleConfig { Id = Guid.NewGuid(), VersionName = "VFe34", Color = "White", InteriorType = "Black Fabric", BasePrice = 30000, WarrantyPeriod = 84 }
            };
        }
    }
}