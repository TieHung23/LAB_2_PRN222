using EVDMS.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EVDMS.BLL.Services.Abstractions
{
    public interface IAccountService
    {
        Task<Account> Login(string email, string password); 
        Task<IEnumerable<Account>> GetAccounts(string searchTerm);
        Task<Account> CreateAccountAsync(Account newAccount);
        Task<Account> GetAccountByIdAsync(Guid id);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(Guid id);
        Task<Account> GetAccountByIdWithDetailsAsync(Guid id);
        Task<IEnumerable<Account>> GetDeletedAccountsAsync();
        Task RestoreAccountAsync(Guid id);
        Task<IEnumerable<Account>> GetAccountsByDealerAsync(Guid dealerId);
        Task<bool> EmailExistsAsync(string email); 
    }
}
