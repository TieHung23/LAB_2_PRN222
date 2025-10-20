using EVDMS.Core.Entities;

namespace EVDMS.BLL.Services.Abstractions
{
    public interface IPaymentService
    {
        public Task<Payment> GetPaymentByOrderIdAsync( Guid orderId );

        public Task<List<Payment>> GetAllPaymentAsync();
    }
}
