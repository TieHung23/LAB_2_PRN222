using System.ComponentModel.DataAnnotations;

namespace EVDMS.Core.Entities;

public class VehicleConfig
{
    [Key] public int Id { get; set; }

    public string VersionName { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public string InteriorType { get; set; } = string.Empty;

    public bool IsDeleted { get; set; } = false;

    public decimal BasePrice { get; set; }

    public int WarrantyPeriod { get; set; }
}