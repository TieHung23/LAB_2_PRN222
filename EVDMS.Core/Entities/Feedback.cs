using System.ComponentModel.DataAnnotations;

namespace EVDMS.Core.Entities;

public class Feedback
{
    [Key] public int Id { get; set; }

    public int CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public string Detail { get; set; } = string.Empty;
}