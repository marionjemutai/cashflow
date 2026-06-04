using System;

namespace CashflowGateway.Domain;

public class InventoryMovement
{
    public Guid Id { get; set; }
    public Guid? ProductId { get; set; }
    public string? Type { get; set; } // SALE, RESTOCK, ADJUSTMENT
    public int Quantity { get; set; }
    public Guid? ReferenceId { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
}