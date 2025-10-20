using EVDMS.Core.Entities;
using System.Linq.Expressions; // Cần cho AnyAsync

namespace EVDMS.DAL.Repositories.Abstractions
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(Guid id);
        Task<Customer> AddAsync(Customer entity);
        Task UpdateAsync(Customer entity);
        Task DeleteAsync(Customer entity);
        Task<bool> AnyAsync(Expression<Func<Customer, bool>> predicate);
    }
}