using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EVDMS.Presentation.Pages.Admin
{
    public class VehicleModelsModel : PageModel
    {
        // Bạn có thể inject services ở đây
        // private readonly IVehicleModelService _vehicleService;

        // public VehicleModelsModel(IVehicleModelService vehicleService)
        // {
        //     _vehicleService = vehicleService;
        // }

        // Tạo một property để giữ danh sách xe (sẽ được load trong OnGet)
        // public IEnumerable<VehicleModel> VehicleModels { get; set; }

        public void OnGet()
        {
            // Đây là nơi bạn sẽ gọi service để lấy dữ liệu
            // Ví dụ: VehicleModels = _vehicleService.GetAll();

            // Dữ liệu trong file .cshtml ở trên hiện là dữ liệu giả (hard-coded)
            // Bạn sẽ thay thế nó bằng vòng lặp @foreach (var model in Model.VehicleModels)
        }
    }
}