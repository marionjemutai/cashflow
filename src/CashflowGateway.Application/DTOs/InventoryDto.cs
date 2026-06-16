using System;

namespace CashflowGateway.Application;

public class CreateProductDto
{
    public Guid?   StoreId  { get; set; }
    public string  Name     { get; set; } = string.Empty;
    public decimal Price    { get; set; }
    public int     Stock    { get; set; } = 0;
}

public class UpdateProductDto
{
    public string?  Name     { get; set; }
    public decimal? Price    { get; set; }
    public int?     Stock    { get; set; }
    public bool?    IsActive { get; set; }
}

public class ProductResponseDto
{
    public Guid      Id       { get; set; }
    public Guid?     StoreId  { get; set; }
    public string    Name     { get; set; } = string.Empty;
    public decimal   Price    { get; set; }
    public int       Stock    { get; set; }
    public bool      IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
}

public class InventoryMovementResponseDto
{
    public Guid      Id          { get; set; }
    public Guid?     ProductId   { get; set; }
    public string?   Type        { get; set; }
    public int       Quantity    { get; set; }
    public Guid?     ReferenceId { get; set; }
    public DateTime? CreatedAt   { get; set; }
}