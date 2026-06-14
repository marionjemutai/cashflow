using System;
using System.Collections.Generic;

namespace CashflowGateway.Application;

public class LedgerEntryDto
{
    public Guid      Id          { get; set; }
    public Guid      StoreId     { get; set; }
    public Guid      DeviceId    { get; set; }
    public decimal   TotalAmount { get; set; }
    public string    Status      { get; set; } = string.Empty;
    public DateTime? CreatedAt   { get; set; }
}

public class LedgerSummaryDto
{
    public int     TotalTransactions { get; set; }
    public decimal TotalRevenue      { get; set; }
    public DateTime? From            { get; set; }
    public DateTime? To              { get; set; }
}