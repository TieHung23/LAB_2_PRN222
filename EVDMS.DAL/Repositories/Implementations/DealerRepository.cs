using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions; 
using System.Text;
using System.Threading.Tasks;
using EVDMS.Core.Entities;
using EVDMS.DAL.Database;
using EVDMS.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EVDMS.DAL.Repositories.Implementations
{
    public class DealerRepository : IDealerRepository
    {
        private readonly ApplicationDbContext _context;

        public DealerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dealer>> GetAllAsync()
        {
            return await _context.Dealers
                                 .Where(d => !d.IsDeleted) 
                                 .AsNoTracking() 
                                 .ToListAsync();
        }


        public async Task<Dealer?> GetByIdAsync(Guid id)
        {
            return await _context.Dealers
                                 .Where(d => !d.IsDeleted)
                                 .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Dealer> AddAsync(Dealer entity)
        {
            await _context.Dealers.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity; 
        }

        public async Task UpdateAsync(Dealer entity)
        {
            
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Dealer entity)
        {

            _context.Dealers.Remove(entity);

            await _context.SaveChangesAsync(); 
        }

        public async Task<bool> AnyAsync(Expression<Func<Dealer, bool>> predicate)
        {
            return await _context.Dealers
                                 .Where(d => !d.IsDeleted) 
                                 .AnyAsync(predicate);
        }
    }
}