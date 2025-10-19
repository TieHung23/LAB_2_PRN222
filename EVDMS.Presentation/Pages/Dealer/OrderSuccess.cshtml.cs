using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QRCoder; // <--- SỬ DỤNG THƯ VIỆN QR CHÚNG TA ĐÃ CÀI
using System.IO;

namespace EVDMS.Presentation.Pages.Dealer
{
    [Authorize(Roles = "Dealer Staff, Dealer Manager")]
    public class OrderSuccessModel : PageModel
    {
        private readonly IOrderService _orderService;

        public OrderSuccessModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public Order Order { get; set; }
        public string QrCodeAsBase64 { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid orderId)
        {
            // Lấy đơn hàng VÀ thông tin thanh toán, khách hàng (chúng ta đã sửa hàm GetByIdAsync)
            Order = await _orderService.GetByIdAsync(orderId);

            if (Order == null || Order.Payment == null || Order.Customer == null)
            {
                return NotFound();
            }

            // Tạo chuỗi nội dung cho QR Code
            // Đây là ví dụ đơn giản, thực tế có thể dùng chuẩn VietQR
            string qrPayload = $"Order ID: {Order.Id}\n" +
                               $"Customer: {Order.Customer.FullName}\n" +
                               $"Amount: {Order.Payment.FinalPrice.ToString("N0")} VND";

            // Tạo QR Code
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrPayload, QRCodeGenerator.ECCLevel.Q);

            // Chuyển sang Base64 để hiển thị trong <img>
            Base64QRCode qrCode = new Base64QRCode(qrCodeData);
            QrCodeAsBase64 = qrCode.GetGraphic(20);

            return Page();
        }
    }
}