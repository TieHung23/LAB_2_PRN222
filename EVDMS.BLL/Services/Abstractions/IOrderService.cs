using EVDMS.Core.Entities;
using AccountEntity = EVDMS.Core.Entities.Account;
namespace EVDMS.BLL.Services.Abstractions
{
    public interface IOrderService
    {
        Task<decimal> GetTotalRevenueByStaffIdAsync(Guid staffId);
        Task<Order> CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersByStaffIdAsync(Guid staffId);
        Task<Order> GetByIdAsync(Guid id);
        Task<decimal> GetTotalRevenueByDealerIdAsync(Guid dealerId);
        Task<IEnumerable<Order>> GetOrdersByDealerIdAsync(Guid dealerId);
        Task<List<(AccountEntity Staff, decimal Revenue)>> GetStaffRevenuesByDealerAsync(Guid dealerId); 
        Task<List<Order>> GetAllOrder();
    }
}
