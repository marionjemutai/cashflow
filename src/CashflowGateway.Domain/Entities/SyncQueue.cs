using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace CashflowGateway.Domain;

public class SyncQueue
{
    public Guid Id { get; set; }
    public Guid? DeviceId { get; set; }
    public string? Payload { get; set; } 
    public string? Type { get; set; } 
    public string Status { get; set; } = "PENDING"; 
    public int? RetryCount { get; set; } = 0;
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? SyncedAt { get; set; }
}