using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.Entities;
using EVDMS.DAL.Repositories.Abstractions;

namespace EVDMS.BLL.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<Inventory>> GetAvailableStockAsync(Guid dealerId)
        {
            return await _inventoryRepository.GetAvailableStockAsync(dealerId);
        }

        public async Task<IEnumerable<Inventory>> GetByDealerIdAsync(Guid dealerId)
        {
            return await _inventoryRepository.GetByDealerIdAsync(dealerId);
        }

        public async Task<Inventory> GetByIdAsync(Guid id)
        {
            return await _inventoryRepository.GetByIdAsync(id);
        }
    }
}