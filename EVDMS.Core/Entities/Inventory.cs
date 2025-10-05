using System.ComponentModel.DataAnnotations;

namespace EVDMS.Core.Entities;

public class Inventory
{
    [Key] public int Id { get; set; }

    public int VehicleModelId { get; set; }

    public VehicleModel? VehicleModel { get; set; }

    public int DealerId { get; set; }

    public Dealer? Dealer { get; set; }

    public bool IsSale { get; set; } = true;

    public string Description { get; set; } = string.Empty;
}