using System;
using System.Collections.Generic;

namespace CashflowGateway.Application;

public class SyncPullResponseDto
{
    public DateTime ServerTime { get; set; }

    public List<ProductSyncDto> UpdatedProducts { get; set; } = new();
}

public class ProductSyncDto
{
    public Guid   Id        { get; set; }
    public string Name      { get; set; } = string.Empty;
    public decimal Price    { get; set; }
    public int    Stock     { get; set; }
    public DateTime UpdatedAt { get; set; }
}