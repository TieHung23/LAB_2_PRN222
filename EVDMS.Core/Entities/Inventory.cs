using EVDMS.Core.CommonEntities;
using System.ComponentModel.DataAnnotations;

namespace EVDMS.Core.Entities;

public class Inventory : UpdatedCommon
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public Guid VehicleModelId { get; set; }

    [Required]
    public Guid DealerId { get; set; }

    public bool IsSale { get; set; } = true;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;


    public VehicleModel? VehicleModel { get; set; }

    public Dealer? Dealer { get; set; }

}