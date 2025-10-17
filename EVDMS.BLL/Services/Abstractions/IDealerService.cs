using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EVDMS.Core.Entities; 

namespace EVDMS.BLL.Services.Abstractions
{
    public interface IDealerService
    {
        Task<IEnumerable<Dealer>> GetAllAsync();
        Task<Dealer?> GetDealerByIdAsync(Guid id); 
        Task<Dealer> CreateDealerAsync(Dealer newDealer);
        Task UpdateDealerAsync(Dealer dealer);
        Task DeleteDealerAsync(Guid id);
        Task<bool> CodeExistsAsync(string code, Guid? excludeId = null);
        Task<bool> NameExistsAsync(string name, Guid? excludeId = null);
        Task<bool> EmailExistsAsync(string email, Guid? excludeId = null);

        Task<bool> HasAssociatedAccountsAsync(Guid dealerId);
    }
}