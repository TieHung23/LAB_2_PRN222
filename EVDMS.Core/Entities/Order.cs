using System.ComponentModel.DataAnnotations;

namespace EVDMS.Core.Entities;

public class Order
{
    [Key] public int Id { get; set; }

    public int CustomerId { get; set; }

    public int AccountId { get; set; }

    public int InventoryId { get; set; }

    public int? PromotionId { get; set; }

    public Customer? Customer { get; set; }

    public Account? Account { get; set; }

    public Inventory? Inventory { get; set; }

    public Promotion? Promotion { get; set; }

    public Payment? Payment { get; set; }
}