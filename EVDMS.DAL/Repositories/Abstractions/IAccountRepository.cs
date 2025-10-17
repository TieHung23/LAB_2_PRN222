using EVDMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions; // <-- Add this using
using System.Threading.Tasks;

namespace EVDMS.DAL.Repositories.Abstractions
{
    public interface IAccountRepository
    {
        Task<Account?> GetByEmail(string email); // <-- Added '?' for nullability
        Task<IEnumerable<Account>> GetAccounts(string searchTerm);
        Task<Account> AddAsync(Account newAccount); // Renamed for consistency
        Task<Account?> GetByIdAsync(Guid id); // <-- Added '?' for nullability
        Task UpdateAsync(Account account);
        Task DeleteAsync(Account account);
        Task<Account?> GetByIdWithDetailsAsync(Guid id); // <-- Added '?' for nullability
        Task<IEnumerable<Account>> GetDeletedAccountsAsync();
        Task RestoreAsync(Account account);
        Task<IEnumerable<Account>> GetAccountsByDealerAsync(Guid dealerId);
        Task<bool> EmailExistsAsync(string email);

        // --- ADD THIS METHOD SIGNATURE ---
        Task<bool> AnyAsync(Expression<Func<Account, bool>> predicate);
    }
}