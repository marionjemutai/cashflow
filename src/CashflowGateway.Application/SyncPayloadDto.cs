
using System;
using System.Collections.Generic;

namespace CashflowGateway.Application;


public class SyncPayloadDto
{
    public Guid DeviceId { get; set; }
    public DateTime LastSyncTime { get; set; }
    public List<OfflineTransactionDto> OfflineTransactions { get; set; } = new();
}

public class OfflineTransactionDto
{
    public Guid Id { get; set; }
    public Guid StoreId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<OfflineTransactionItemDto> Items { get; set; } = new();
}

public class OfflineTransactionItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
