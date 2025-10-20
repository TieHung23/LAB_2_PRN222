using EVDMS.Core.Entities;
namespace EVDMS.BLL.Services.Abstractions
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetActivePromotionsAsync();

        Task<IEnumerable<Promotion>> GetAllAsync();
    }
}
