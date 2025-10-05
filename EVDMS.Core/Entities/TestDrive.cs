using System.ComponentModel.DataAnnotations;

namespace EVDMS.Core.Entities;

public class TestDrive
{
    [Key] public int Id { get; set; }

    public int VehicleModelId { get; set; }

    public VehicleModel? VehicleModel { get; set; }

    public int CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public DateTime? ScheduledDateTime { get; set; }

    public bool IsSuccess { get; set; } = false;

    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; } = false;
}