using EVDMS.Core.Entities;

namespace EVDMS.DAL.Repositories.Abstractions
{
    public interface IFeatureRepository
    {
        Task<List<Feature>> GetAllFeatureAsync();
    }
}
