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



public class SyncPullResponseDto
{
    public DateTime ServerTime { get; set; }
    public List<ProductSyncDto> UpdatedProducts { get; set; } = new();
}

public class ProductSyncDto
{
    public Guid      Id        { get; set; }
    public string    Name      { get; set; } = string.Empty;
    public decimal   Price     { get; set; }
    public int       Stock     { get; set; }
    public DateTime? CreatedAt { get; set; }
}