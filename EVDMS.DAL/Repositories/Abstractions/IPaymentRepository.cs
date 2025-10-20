using EVDMS.Core.Entities;

namespace EVDMS.DAL.Repositories.Abstractions
{
    public interface IPaymentRepository
    {
        Task<Payment> GetPaymentByOrderIdAsync( Guid orderId );

        Task<List<Payment>> GetAllPaymentAsync();
    }
}
