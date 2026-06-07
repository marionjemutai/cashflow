using Microsoft.EntityFrameworkCore;
using CashflowGateway.Domain;

namespace CashflowGateway.Application;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<Store> Stores { get; }
    DbSet<Device> Devices { get; }
    DbSet<Transaction> Transactions { get; }
    DbSet<TransactionItem> TransactionItems { get; }
    DbSet<InventoryMovement> InventoryMovements { get; }
    DbSet<SyncQueue> SyncQueues { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}