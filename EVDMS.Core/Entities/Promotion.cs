using System.ComponentModel.DataAnnotations;

namespace EVDMS.Core.Entities;

public class Promotion
{
    [Key] public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; } = false;

    [Range(0, 50)] public int PercentDiscount { get; set; }
}