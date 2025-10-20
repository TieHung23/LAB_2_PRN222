using EVDMS.Core.Entities;

namespace EVDMS.DAL.Repositories.Abstractions
{
    public interface IInventoryRepository
    {
        Task<IEnumerable<Inventory>> GetAvailableStockAsync(Guid dealerId);
        Task<Inventory> GetByIdAsync(Guid id);
        Task<bool> CheckIfVehicleModelExistsInInventory(Guid vehicleModelId);

        Task<IEnumerable<Inventory>> GetByDealerIdAsync(Guid dealerId);
    }
}
