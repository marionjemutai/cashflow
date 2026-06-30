using System;
using System.Collections.Generic;

namespace CashflowGateway.Application;

public class DailySalesDto
{
    public DateTime Date             { get; set; }
    public int      TransactionCount { get; set; }
    public decimal  TotalRevenue     { get; set; }
}


public class ProductSalesDto
{
    public Guid    ProductId     { get; set; }
    public string  ProductName   { get; set; } = string.Empty;
    public int     TotalQuantity { get; set; }
    public decimal TotalRevenue  { get; set; }
}


public class DeviceSalesDto
{
    public Guid    DeviceId          { get; set; }
    public string  DeviceName        { get; set; } = string.Empty;
    public int     TransactionCount  { get; set; }
    public decimal TotalRevenue      { get; set; }
}