using System;

namespace CashflowGateway.Domain;

public class TransactionItem
{
    public Guid Id { get; set; }
    public Guid TransactionId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}