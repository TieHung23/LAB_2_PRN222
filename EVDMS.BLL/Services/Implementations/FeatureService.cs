using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using EVDMS.DAL.Repositories.Abstractions;

namespace EVDMS.BLL.Services.Implementations
{
    public class FeatureService : IFeatureService
    {
        private readonly IFeatureRepository _featureRepository;

        public FeatureService( IFeatureRepository featureRepository )
        {
            _featureRepository = featureRepository;
        }

        public async Task<List<Feature>> GetAllFeatureAsync()
        {
            return await _featureRepository.GetAllFeatureAsync();
        }
    }
}
