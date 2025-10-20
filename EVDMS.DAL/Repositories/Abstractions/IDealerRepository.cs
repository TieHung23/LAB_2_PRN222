using EVDMS.Core.Entities;
using System.Linq.Expressions; // Cần cho AnyAsync

namespace EVDMS.DAL.Repositories.Abstractions
{
    public interface IDealerRepository
    {
        Task<IEnumerable<Dealer>> GetAllAsync();

        Task<Dealer?> GetByIdAsync(Guid id);
        Task<Dealer> AddAsync(Dealer entity);
        Task UpdateAsync(Dealer entity);
        Task DeleteAsync(Dealer entity);

        Task<bool> AnyAsync(Expression<Func<Dealer, bool>> predicate);
    }
}