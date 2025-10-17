using EVDMS.Core.Entities;
using EVDMS.DAL.Database;
using EVDMS.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions; // <-- Add this using
using System.Threading.Tasks;

namespace EVDMS.DAL.Repositories.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;

        public AccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Account?> GetByEmail(string email)
        {
            return await _context.Accounts
                                 .Include(a => a.Role) // Include Role for login check
                                 .Include(a => a.Dealer) // Include Dealer if needed
                                 .FirstOrDefaultAsync(a => a.Email.ToLower() == email.ToLower() && !a.IsDeleted);
        }

        public async Task<IEnumerable<Account>> GetAccounts(string searchTerm)
        {
            var query = _context.Accounts
                                .Include(a => a.Role)
                                .Include(a => a.Dealer)
                                .Where(a => !a.IsDeleted);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(a => a.FullName.Contains(searchTerm) || a.Email.Contains(searchTerm));
            }

            return await query.AsNoTracking().ToListAsync();
        }

        // Renamed from CreateAccountAsync
        public async Task<Account> AddAsync(Account newAccount)
        {
            await _context.Accounts.AddAsync(newAccount);
            await _context.SaveChangesAsync();
            return newAccount;
        }

        public async Task<Account?> GetByIdAsync(Guid id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task UpdateAsync(Account account)
        {
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Account account)
        {
            // Use soft delete
            account.IsDeleted = true;
            account.IsActive = false; // Optionally deactivate on delete
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task<Account?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.Accounts
                                 .Include(a => a.Role)
                                 .Include(a => a.Dealer)
                                 .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<IEnumerable<Account>> GetDeletedAccountsAsync()
        {
            return await _context.Accounts
                               .Include(a => a.Role)
                               .Include(a => a.Dealer)
                               .Where(a => a.IsDeleted)
                               .AsNoTracking()
                               .ToListAsync();
        }

        public async Task RestoreAsync(Account account)
        {
            account.IsDeleted = false;
            account.IsActive = true; // Optionally reactivate on restore
            _context.Accounts.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Account>> GetAccountsByDealerAsync(Guid dealerId)
        {
            return await _context.Accounts
                                .Include(a => a.Role)
                                .Where(a => a.DealerId == dealerId && !a.IsDeleted)
                                .AsNoTracking()
                                .ToListAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Accounts.AnyAsync(a => a.Email.ToLower() == email.ToLower() && !a.IsDeleted);
        }

        // --- ADD THIS IMPLEMENTATION ---
        public async Task<bool> AnyAsync(Expression<Func<Account, bool>> predicate)
        {
            // Optionally add .Where(a => !a.IsDeleted) if checks should ignore deleted accounts
            return await _context.Accounts.AnyAsync(predicate);
        }
    }
}