using EVDMS.Core.Entities;

namespace EVDMS.DAL.Repositories.Abstractions
{
    public interface IPromotionRepository
    {
        Task<IEnumerable<Promotion>> GetActivePromotionsAsync();
        Task<Promotion> GetByIdAsync(Guid id);

        Task<IEnumerable<Promotion>> GetAllAsync();
    }
}