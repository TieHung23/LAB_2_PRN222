using EVDMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions; // Cần cho AnyAsync
using System.Threading.Tasks;

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