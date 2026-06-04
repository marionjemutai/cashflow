using System;

namespace CashflowGateway.Domain;

public class Transaction
{
    public Guid Id { get; set; } // Browser-generated UUID
    public Guid StoreId { get; set; }
    public Guid DeviceId { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "COMPLETED";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}