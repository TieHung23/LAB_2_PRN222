using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using EVDMS.DAL.Repositories.Abstractions;

namespace EVDMS.BLL.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Account> Login(string email, string password)
        {

            var account = await _accountRepository.GetByEmail(email);
            if (account == null)
            {
                return null;
            }


            bool isPasswordVerified = BCrypt.Net.BCrypt.Verify(password, account.HashedPassword);


            if (isPasswordVerified)
            {
                if (account.IsDeleted == false && account.IsActive == true)
                {
                    return account;
                }
            }


            return null;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _accountRepository.EmailExistsAsync(email);
        }

        // Các phương thức còn lại giữ nguyên
        public async Task<IEnumerable<Account>> GetAccounts(string searchTerm)
        {
            return await _accountRepository.GetAccounts(searchTerm);
        }

        public async Task<Account> CreateAccountAsync(Account newAccount)
        {
            newAccount.HashedPassword = BCrypt.Net.BCrypt.HashPassword(newAccount.HashedPassword);
            return await _accountRepository.AddAsync(newAccount);
        }

        public async Task<Account> GetAccountByIdAsync(Guid id)
        {
            return await _accountRepository.GetByIdAsync(id);
        }

        public async Task UpdateAccountAsync(Account account)
        {
            await _accountRepository.UpdateAsync(account);
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account != null)
            {
                await _accountRepository.DeleteAsync(account);
            }
        }
        public async Task<Account> GetAccountByIdWithDetailsAsync(Guid id)
        {
            return await _accountRepository.GetByIdWithDetailsAsync(id);
        }

        public async Task<IEnumerable<Account>> GetDeletedAccountsAsync()
        {
            return await _accountRepository.GetDeletedAccountsAsync();
        }

        public async Task RestoreAccountAsync(Guid id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account != null)
            {
                await _accountRepository.RestoreAsync(account);
            }
        }

        public async Task<IEnumerable<Account>> GetAccountsByDealerAsync(Guid dealerId)
        {
            return await _accountRepository.GetAccountsByDealerAsync(dealerId);
        }
    }
}