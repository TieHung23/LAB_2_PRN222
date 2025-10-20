using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using EVDMS.DAL.Repositories.Abstractions;

namespace EVDMS.BLL.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService( IPaymentRepository paymentRepository )
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<List<Payment>> GetAllPaymentAsync()
        {
            return await _paymentRepository.GetAllPaymentAsync();
        }

        public async Task<Payment> GetPaymentByOrderIdAsync( Guid orderId )
        {
            return await _paymentRepository.GetPaymentByOrderIdAsync( orderId );

        }
    }
}
