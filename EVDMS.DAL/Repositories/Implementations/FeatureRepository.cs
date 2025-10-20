using EVDMS.Core.Entities;
using EVDMS.DAL.Database;
using EVDMS.DAL.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EVDMS.DAL.Repositories.Implementations
{
    public class FeatureRepository : IFeatureRepository
    {
        private readonly ApplicationDbContext _context;

        public FeatureRepository( ApplicationDbContext context )
        {
            _context = context;
        }

        public async Task<List<Feature>> GetAllFeatureAsync()
        {
            return await _context.Features.ToListAsync();
        }
    }
}
