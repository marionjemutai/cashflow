
using Microsoft.EntityFrameworkCore;
using CashflowGateway.Domain;

namespace CashflowGateway.Infrastructure.Data;

public class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionItem> TransactionItems { get; set; }
    public DbSet<InventoryMovement> InventoryMovements { get; set; }
    public DbSet<SyncQueue> SyncQueues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable("users");
        modelBuilder.Entity<Store>().ToTable("stores");
        modelBuilder.Entity<Device>().ToTable("devices");
        modelBuilder.Entity<Transaction>().ToTable("transactions");
        modelBuilder.Entity<TransactionItem>().ToTable("transaction_items");
        modelBuilder.Entity<InventoryMovement>().ToTable("inventory_movements");
        modelBuilder.Entity<SyncQueue>().ToTable("sync_queue");

        modelBuilder.Entity<Store>()
            .Property(s => s.CretedAt)
            .HasColumnName("creted_at");
    }
}

