using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace CashflowGateway.Domain;

public class Store
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}