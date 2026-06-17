using System;
using System.Collections.Generic;

namespace CashflowGateway.Application;

public class ReceiptDto
{
    public Guid      TransactionId { get; set; }
    public string    StoreName     { get; set; } = string.Empty;
    public string?   StoreLocation { get; set; }
    public string    Status        { get; set; } = string.Empty;
    public DateTime? CreatedAt     { get; set; }
    public decimal   TotalAmount   { get; set; }
    public List<ReceiptItemDto> Items { get; set; } = new();
}

public class ReceiptItemDto
{
    public string  ProductName { get; set; } = string.Empty;
    public int     Quantity    { get; set; }
    public decimal UnitPrice   { get; set; }
    public decimal Subtotal    { get; set; }
}