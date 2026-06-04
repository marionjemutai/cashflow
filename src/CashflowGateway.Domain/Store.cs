using System;

namespace CashflowGateway.Domain;

public class Store
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public DateTime? CretedAt { get; set; } = DateTime.UtcNow;
}