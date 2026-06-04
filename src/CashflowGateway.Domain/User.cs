using System;

namespace CashflowGateway.Domain;

public class User
{
    public Guid Id { get; set; }
    public Guid? StoreId { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    public string Role { get; set; } = "CASHIER"; 
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
}