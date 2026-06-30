using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CashflowGateway.Application;

public class ReportService : IReportService
{
    private readonly IAppDbContext _context;

    public ReportService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<DailySalesDto>> GetDailySalesAsync(DateTime? from, DateTime? to)
    {
        var query = _context.Transactions.AsQueryable();

        if (from.HasValue)
            query = query.Where(t => t.CreatedAt >= from.Value);

        if (to.HasValue)
            query = query.Where(t => t.CreatedAt <= to.Value);

        var transactions = await query
            .Select(t => new { t.CreatedAt, t.TotalAmount })
            .ToListAsync();

        return transactions
            .Where(t => t.CreatedAt.HasValue)
            .GroupBy(t => t.CreatedAt!.Value.Date)
            .Select(g => new DailySalesDto
            {
                Date             = g.Key,
                TransactionCount = g.Count(),
                TotalRevenue     = g.Sum(t => t.TotalAmount)
            })
            .OrderByDescending(d => d.Date)
            .ToList();
    }

    public async Task<List<ProductSalesDto>> GetTopProductsAsync(DateTime? from, DateTime? to, int limit)
    {
        var itemsQuery = _context.TransactionItems.AsQueryable();

        if (from.HasValue || to.HasValue)
        {
            var txQuery = _context.Transactions.AsQueryable();
            if (from.HasValue) txQuery = txQuery.Where(t => t.CreatedAt >= from.Value);
            if (to.HasValue)   txQuery = txQuery.Where(t => t.CreatedAt <= to.Value);

            var txIds = await txQuery.Select(t => t.Id).ToListAsync();
            itemsQuery = itemsQuery.Where(i => txIds.Contains(i.TransactionId));
        }

        var items = await itemsQuery.ToListAsync();
        var products = await _context.Products.ToListAsync();

        return items
            .GroupBy(i => i.ProductId)
            .Select(g =>
            {
                var product = products.FirstOrDefault(p => p.Id == g.Key);
                return new ProductSalesDto
                {
                    ProductId     = g.Key,
                    ProductName   = product?.Name ?? "Unknown Product",
                    TotalQuantity = g.Sum(i => i.Quantity),
                    TotalRevenue  = g.Sum(i => i.Quantity * i.UnitPrice)
                };
            })
            .OrderByDescending(p => p.TotalQuantity)
            .Take(limit)
            .ToList();
    }


    public async Task<List<DeviceSalesDto>> GetDeviceSalesAsync(DateTime? from, DateTime? to)
    {
        var query = _context.Transactions.AsQueryable();

        if (from.HasValue)
            query = query.Where(t => t.CreatedAt >= from.Value);

        if (to.HasValue)
            query = query.Where(t => t.CreatedAt <= to.Value);

        var transactions = await query.ToListAsync();
        var devices = await _context.Devices.ToListAsync();

        return transactions
            .GroupBy(t => t.DeviceId)
            .Select(g =>
            {
                var device = devices.FirstOrDefault(d => d.Id == g.Key);
                return new DeviceSalesDto
                {
                    DeviceId         = g.Key,
                    DeviceName       = device?.DeviceName ?? "Unknown Device",
                    TransactionCount = g.Count(),
                    TotalRevenue     = g.Sum(t => t.TotalAmount)
                };
            })
            .OrderByDescending(d => d.TotalRevenue)
            .ToList();
    }
}