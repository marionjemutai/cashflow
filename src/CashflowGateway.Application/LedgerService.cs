using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CashflowGateway.Application;

public class LedgerService : ILedgerService
{
    private readonly IAppDbContext _context;

    public LedgerService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<LedgerEntryDto>> GetLedgerAsync(DateTime? from, DateTime? to)
    {
        var query = _context.Transactions.AsQueryable();
        if (from.HasValue)
            query = query.Where(t => t.CreatedAt >= from.Value);

        if (to.HasValue)
            query = query.Where(t => t.CreatedAt <= to.Value);

        return await query
            .OrderByDescending(t => t.CreatedAt)
            .Select(t => new LedgerEntryDto
            {
                Id          = t.Id,
                StoreId     = t.StoreId,
                DeviceId    = t.DeviceId,
                TotalAmount = t.TotalAmount,
                Status      = t.Status,
                CreatedAt   = t.CreatedAt
            })
            .ToListAsync();
    }


    public async Task<LedgerSummaryDto> GetSummaryAsync(DateTime? from, DateTime? to)
    {
        var query = _context.Transactions.AsQueryable();

        if (from.HasValue)
            query = query.Where(t => t.CreatedAt >= from.Value);

        if (to.HasValue)
            query = query.Where(t => t.CreatedAt <= to.Value);

        var total   = await query.CountAsync();
        var revenue = await query.SumAsync(t => t.TotalAmount);

        return new LedgerSummaryDto
        {
            TotalTransactions = total,
            TotalRevenue      = revenue,
            From              = from,
            To                = to
        };
    }
}