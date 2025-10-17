using System;
using System.ComponentModel.DataAnnotations;
using EVDMS.Core.CommonEntities;

namespace EVDMS.Core.Entities;

public class Inventory : UpdatedCommon
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid VehicleModelId { get; set; }

    public VehicleModel? VehicleModel { get; set; }

    public Guid DealerId { get; set; }

    public Dealer? Dealer { get; set; }

    public Guid VehicleConfigId { get; set; }

    public VehicleConfig? VehicleConfig { get; set; }

    public int StockQuantity { get; set; }

    public bool IsSale { get; set; } = true;

    public string Description { get; set; } = string.Empty;
}
