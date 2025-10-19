using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVDMS.Core.Entities;
namespace EVDMS.BLL.Services.Abstractions
{
    public interface IInventoryService
    {
        Task<IEnumerable<Inventory>> GetAvailableStockAsync(Guid dealerId);

        Task<IEnumerable<Inventory>> GetByDealerIdAsync(Guid dealerId);

        Task<Inventory> GetByIdAsync(Guid id);
    }
}
