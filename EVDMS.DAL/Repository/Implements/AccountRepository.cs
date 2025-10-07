using EVDMS.Core.Entities;
using EVDMS.Core.Enums;
using EVDMS.DAL.Database;
using EVDMS.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EVDMS.DAL.Repository.Implements;

public class AccountRepository(ApplicationDbContext context) : IAccountRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<List<Account>> GetAllAccountsAsync()
    {
        return await _context.Accounts.Where(x => !x.IsDeleted && x.Active == ActiveEnums.Active).ToListAsync();
    }

    public async Task<Account?> GetAccountByIdAsync(int accountId)
    {
        return await _context.Accounts.FirstOrDefaultAsync(x =>
            x.Id == accountId && !x.IsDeleted && x.Active == ActiveEnums.Active);
    }

    public async Task<Account?> GetAccountByUserNameAsync(string userName)
    {
        return await _context.Accounts.FirstOrDefaultAsync(x =>
            x.UserName == userName && !x.IsDeleted && x.Active == ActiveEnums.Active);
    }

    public async Task<Account?> GetAccountByUserNameAndPasswordAsync(string userName, string password)
    {
        return await _context.Accounts.FirstOrDefaultAsync(x =>
            x.UserName == userName && x.HashedPassword == password && !x.IsDeleted && x.Active == ActiveEnums.Active);
    }

    public async Task<Account> AddAccountAsync(Account account)
    {
        var result = await _context.Accounts.AddAsync(account);

        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<Account> UpdateAccountAsync(Account account)
    {
        _context.Accounts.Update(account);

        await _context.SaveChangesAsync();

        return account;
    }

    public async Task DeleteAccountAsync(Account account)
    {
        var result = await _context.Accounts.FindAsync(account);

        if (result != null) result.IsDeleted = true;

        await _context.SaveChangesAsync();
    }
}