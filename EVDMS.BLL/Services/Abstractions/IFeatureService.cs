using EVDMS.Core.Entities;

namespace EVDMS.BLL.Services.Abstractions
{
    public interface IFeatureService
    {
        Task<List<Feature>> GetAllFeatureAsync();
    }
}
