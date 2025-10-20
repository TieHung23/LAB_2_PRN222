using EVDMS.Core.DTOs;
using EVDMS.Core.Entities;
namespace EVDMS.BLL.Services.Abstractions
{
    public interface IInventoryService
    {
        Task<IEnumerable<Inventory>> GetAvailableStockAsync( Guid dealerId );

        Task<IEnumerable<Inventory>> GetByDealerIdAsync( Guid dealerId );

        Task<Inventory> GetByIdAsync( Guid id );

        Task<List<Inventory>> GetAllInventoryAsync();

        Task<bool> CreateMultipleInventory( CreateInventory inventoryData );
    }
}
