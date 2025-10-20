using EVDMS.Core.Entities;
using EVDMS.DAL.Database;
using EVDMS.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EVDMS.DAL.Repositories.Implementations
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository( ApplicationDbContext context )
        {
            _context = context;
        }

        public async Task<List<Payment>> GetAllPaymentAsync()
        {
            return await _context.Payments.Include( p => p.Order ).ThenInclude( o => o.Account ).ThenInclude( a => a.Dealer ).ToListAsync();
        }

        public async Task<Payment> GetPaymentByOrderIdAsync( Guid orderId )
        {
            return ( await _context.Payments.Where( x => x.OrderId == orderId ).FirstOrDefaultAsync() )!;
        }
    }
}
