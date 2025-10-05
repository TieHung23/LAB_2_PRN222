using EVDMS.Core.Entities;

namespace EVDMS.DAL.Repository.Interfaces;

public interface IAccountRepository
{
    Task<List<Account>> GetAllAccountsAsync();

    Task<Account?> GetAccountByIdAsync(int accountId);

    Task<Account?> GetAccountByUserNameAsync(string userName);

    Task<Account?> GetAccountByUserNameAndPasswordAsync(string userName, string password);

    Task<Account> AddAccountAsync(Account account);

    Task<Account> UpdateAccountAsync(Account account);

    Task DeleteAccountAsync(Account account);
}