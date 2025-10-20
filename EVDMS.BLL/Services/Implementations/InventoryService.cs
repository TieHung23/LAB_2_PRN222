using EVDMS.BLL.Services.Abstractions;
using EVDMS.Core.DTOs;
using EVDMS.Core.Entities;
using EVDMS.DAL.Repositories.Abstractions;

namespace EVDMS.BLL.Services.Implementations
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryService( IInventoryRepository inventoryRepository )
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<bool> CreateMultipleInventory( CreateInventory inventoryData )
        {
            List<Inventory> addedList = new();

            for( int i = 0; i <= inventoryData.StockQuantity; i++ )
            {
                addedList.Add( new Inventory
                {
                    DealerId = inventoryData.DealerId,
                    VehicleModelId = inventoryData.VehicleConfigurationId,
                    IsSale = true,
                    Description = inventoryData.Description
                } );
            }

            var result = await _inventoryRepository.CreateListAsync( addedList );

            return result;
        }

        public async Task<List<Inventory>> GetAllInventoryAsync()
        {
            return await _inventoryRepository.GetAllInventoryAsync();
        }

        public async Task<IEnumerable<Inventory>> GetAvailableStockAsync( Guid dealerId )
        {
            return await _inventoryRepository.GetAvailableStockAsync( dealerId );
        }

        public async Task<IEnumerable<Inventory>> GetByDealerIdAsync( Guid dealerId )
        {
            return await _inventoryRepository.GetByDealerIdAsync( dealerId );
        }

        public async Task<Inventory> GetByIdAsync( Guid id )
        {
            return await _inventoryRepository.GetByIdAsync( id );
        }
    }
}