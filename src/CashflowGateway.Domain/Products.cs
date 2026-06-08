using System;

namespace CashflowGateway.Domain;

public class Product
{
    public Guid    Id            { get; set; }
    public Guid?   StoreId       { get; set; }
    public string  Name          { get; set; } = string.Empty;
    public decimal Price         { get; set; }
    public int     StockQuantity { get; set; } = 0;
    public bool    IsActive      { get; set; } = true;
    public DateTime? CreatedAt   { get; set; } = DateTime.UtcNow;
}