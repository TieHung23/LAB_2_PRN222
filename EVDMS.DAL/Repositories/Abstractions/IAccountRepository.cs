using EVDMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EVDMS.DAL.Repositories.Abstractions
{
    public interface IAccountRepository
    {
        Task<Account> AddAsync(Account newAccount);
        Task<Account> GetByEmail(string email); // Chỉ sử dụng phương thức này
        Task<IEnumerable<Account>> GetAccounts(string searchTerm);
        Task<Account> GetByIdAsync(Guid id);
        Task UpdateAsync(Account account);
        Task DeleteAsync(Account account);
        Task<Account> GetByIdWithDetailsAsync(Guid id);
        Task<IEnumerable<Account>> GetDeletedAccountsAsync();
        Task RestoreAsync(Account account);
        Task<IEnumerable<Account>> GetAccountsByDealerAsync(Guid dealerId);
        Task<bool> EmailExistsAsync(string email); // Thống nhất tên
    }
}
