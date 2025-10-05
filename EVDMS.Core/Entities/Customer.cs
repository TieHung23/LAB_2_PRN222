using System.ComponentModel.DataAnnotations;

namespace EVDMS.Core.Entities;

public class Customer
{
    [Key] public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string IdCardNumber { get; set; } = string.Empty;
}