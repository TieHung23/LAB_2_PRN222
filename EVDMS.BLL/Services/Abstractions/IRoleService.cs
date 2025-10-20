using EVDMS.Core.Entities;

namespace EVDMS.BLL.Services.Abstractions
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllAsync();

        Task<Role> GetByIdAsync(Guid id);
    }
}