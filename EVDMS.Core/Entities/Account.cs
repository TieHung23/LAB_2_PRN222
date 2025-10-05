using System.ComponentModel.DataAnnotations;
using EVDMS.Core.Enums;

namespace EVDMS.Core.Entities;

public class Account
{
    [Key] public int Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string HashedPassword { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public ActiveEnums Active { get; set; } = ActiveEnums.Active;

    public RoleEnums Role { get; set; } = RoleEnums.DealerStaff;

    public bool IsDeleted { get; set; } = false;

    public int? DealerId { get; set; }

    public Dealer? Dealer { get; set; }
}