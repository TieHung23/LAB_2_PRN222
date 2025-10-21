using EVDMS.Core.Entities;
using AccountEntity = EVDMS.Core.Entities.Account;
namespace EVDMS.DAL.Repositories.Abstractions
{
    public interface IOrderRepository
    {
        Task<decimal> GetTotalRevenueByStaffIdAsync(Guid staffId);
        Task<Order> AddAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersByStaffIdAsync(Guid staffId);
        Task<Order> GetByIdAsync(Guid id);

        Task<decimal> GetTotalRevenueByDealerIdAsync(Guid dealerId);
        Task<IEnumerable<Order>> GetOrdersByDealerIdAsync(Guid dealerId);
        Task<List<(Account Staff, decimal Revenue)>> GetStaffRevenuesByDealerAsync(Guid dealerId);

        Task<List<Order>> GetAllOrder();

    }
}
