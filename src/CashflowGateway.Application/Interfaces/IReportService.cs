using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CashflowGateway.Application;

public interface IReportService
{
    Task<List<DailySalesDto>>   GetDailySalesAsync(DateTime? from, DateTime? to);
    Task<List<ProductSalesDto>> GetTopProductsAsync(DateTime? from, DateTime? to, int limit);
    Task<List<DeviceSalesDto>>  GetDeviceSalesAsync(DateTime? from, DateTime? to);
}