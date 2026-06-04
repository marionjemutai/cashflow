using System;

namespace CashflowGateway.Domain;

public class Device
{
    public Guid Id { get; set; }
    public Guid StoreId { get; set; }
    public string DeviceName { get; set; } = string.Empty;
    public string DeviceKey { get; set; } = string.Empty;
    public string Status { get; set; } = "OFFLINE"; // ONLINE, OFFLINE
    public DateTime? LastSeen { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}