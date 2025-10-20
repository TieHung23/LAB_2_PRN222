using System.ComponentModel.DataAnnotations;

namespace EVDMS.Core.DTOs
{
    public class CreateInventory
    {
        [Required]
        [Display( Name = "Vehicle Model & Color" )]
        public Guid VehicleConfigurationId
        {
            get; set;
        }

        [Display( Name = "Dealer" )]
        public Guid DealerId
        {
            get; set;
        }

        [Required]
        [Range( 1, int.MaxValue, ErrorMessage = "Quantity must be at least 1." )]
        [Display( Name = "Stock Quantity" )]
        public int StockQuantity
        {
            get; set;
        }

        [StringLength( 500 )]
        public string Description
        {
            get; set;
        }
    }
}
