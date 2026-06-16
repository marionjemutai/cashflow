using System.ComponentModel.DataAnnotations.Schema;

namespace CashflowGateway.Domain;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid StoreId { get; set; }
    public Guid DeviceId { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "COMPLETED";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("synced_at")]
    public DateTime? SyncedAt { get; set; }
}