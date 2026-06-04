
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CashflowGateway.Domain;
// BUSINESS LOGIC SERVICE - This is where you implement the core processing of the sync payload data packets received from the API controller. You can perform database operations, data transformations, and any necessary business rules here before saving to the database.

namespace CashflowGateway.Application;

public class SyncService : ISyncService
{
    private readonly DbContext _context;

   
    public SyncService(DbContext context)
    {
        _context = context;
    }

    public async Task<bool> ProcessSyncPayloadAsync(SyncPayloadDto payload)
    {
        
        var device = await _context.Set<Device>().FirstOrDefaultAsync(d => d.Id == payload.DeviceId);
        if (device != null)
        {
            device.LastSeen = DateTime.UtcNow;
            device.Status = "ONLINE";
        }

       
        foreach (var txDto in payload.OfflineTransactions)
        {
            var exists = await _context.Set<Transaction>().AnyAsync(t => t.Id == txDto.Id);
            if (exists)
            {
                continue; 
            }

            var transaction = new Transaction
            {
                Id = txDto.Id,
                StoreId = txDto.StoreId,
                DeviceId = payload.DeviceId,
                TotalAmount = txDto.TotalAmount,
                CreatedAt = txDto.CreatedAt,
                Status = "COMPLETED"
            };
            _context.Set<Transaction>().Add(transaction);

            foreach (var itemDto in txDto.Items)
            {
                var item = new TransactionItem
                {
                    Id = Guid.NewGuid(),
                    TransactionId = transaction.Id,
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice
                };
                _context.Set<TransactionItem>().Add(item);
            }
        }

        await _context.SaveChangesAsync();
        return true;
    }
}

