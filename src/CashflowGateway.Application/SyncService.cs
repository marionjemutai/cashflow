
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CashflowGateway.Domain;

namespace CashflowGateway.Application;

public class SyncService : ISyncService
{
    private readonly IAppDbContext _context;

    public SyncService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ProcessSyncPayloadAsync(SyncPayloadDto payload)
    {

        var queueEntry = new SyncQueue
        {
            Id       = Guid.NewGuid(),
            DeviceId = payload.DeviceId,
            Payload  = JsonSerializer.Serialize(payload),
            Type     = "TRANSACTION_PUSH",
            Status   = "PROCESSING",
            CreatedAt = DateTime.UtcNow
        };
        _context.SyncQueues.Add(queueEntry);
        await _context.SaveChangesAsync();

        try
        {
            var device = await _context.Devices
                .FirstOrDefaultAsync(d => d.Id == payload.DeviceId);

            if (device != null)
            {
                device.LastSeen = DateTime.UtcNow;
                device.Status   = "ONLINE";
            }
            
            foreach (var txDto in payload.OfflineTransactions)
            {
                var exists = await _context.Transactions
                    .AnyAsync(t => t.Id == txDto.Id);

                if (exists) continue;

                var transaction = new Transaction
                {
                    Id          = txDto.Id,
                    StoreId     = txDto.StoreId,
                    DeviceId    = payload.DeviceId,
                    TotalAmount = txDto.TotalAmount,
                    CreatedAt   = txDto.CreatedAt,
                    Status      = "COMPLETED"
                };
                _context.Transactions.Add(transaction);

                foreach (var itemDto in txDto.Items)
                {
                    var item = new TransactionItem
                    {
                        Id            = Guid.NewGuid(),
                        TransactionId = transaction.Id,
                        ProductId     = itemDto.ProductId,
                        Quantity      = itemDto.Quantity,
                        UnitPrice     = itemDto.UnitPrice
                    };
                    _context.TransactionItems.Add(item);
                }
            }
            queueEntry.Status   = "COMPLETED";
            queueEntry.SyncedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            queueEntry.Status     = "FAILED";
            queueEntry.RetryCount = (queueEntry.RetryCount ?? 0) + 1;
            await _context.SaveChangesAsync();
            return false;
        }
    }
}