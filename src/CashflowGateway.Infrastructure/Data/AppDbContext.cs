
using Microsoft.EntityFrameworkCore;
using CashflowGateway.Domain;
using CashflowGateway.Application;

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
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users");
            entity.Property(u => u.Id).HasColumnName("id");
            entity.Property(u => u.StoreId).HasColumnName("store_id");
            entity.Property(u => u.FullName).HasColumnName("full_name");
            entity.Property(u => u.Email).HasColumnName("email");
            entity.Property(u => u.PasswordHash).HasColumnName("password_hash");
            entity.Property(u => u.Role).HasColumnName("role");
            entity.Property(u => u.CreatedAt).HasColumnName("created_at");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.ToTable("stores");
            entity.Property(s => s.Id).HasColumnName("id");
            entity.Property(s => s.Name).HasColumnName("name");
            entity.Property(s => s.Location).HasColumnName("location");
            entity.Property(s => s.CreatedAt).HasColumnName("creted_at");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.ToTable("devices");
            entity.Property(d => d.Id).HasColumnName("id");
            entity.Property(d => d.StoreId).HasColumnName("store_id");
            entity.Property(d => d.DeviceName).HasColumnName("device_name");
            entity.Property(d => d.DeviceKey).HasColumnName("device_key");
            entity.Property(d => d.Status).HasColumnName("status");
            entity.Property(d => d.LastSeen).HasColumnName("last_seen");
            entity.Property(d => d.CreatedAt).HasColumnName("created_at");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("transactions");
            entity.Property(t => t.Id).HasColumnName("id");
            entity.Property(t => t.StoreId).HasColumnName("store_id");
            entity.Property(t => t.DeviceId).HasColumnName("device_id");
            entity.Property(t => t.TotalAmount).HasColumnName("total_amount");
            entity.Property(t => t.Status).HasColumnName("status");
            entity.Property(t => t.CreatedAt).HasColumnName("created_at");
        });

        modelBuilder.Entity<TransactionItem>(entity =>
        {
            entity.ToTable("transaction_items");
            entity.Property(t => t.Id).HasColumnName("id");
            entity.Property(t => t.TransactionId).HasColumnName("transaction_id");
            entity.Property(t => t.ProductId).HasColumnName("product_id");
            entity.Property(t => t.Quantity).HasColumnName("quantity");
            entity.Property(t => t.UnitPrice).HasColumnName("unit_price");
        });

        modelBuilder.Entity<InventoryMovement>(entity =>
        {
            entity.ToTable("inventory_movements");
            entity.Property(i => i.Id).HasColumnName("id");
            entity.Property(i => i.ProductId).HasColumnName("product_id");
            entity.Property(i => i.Type).HasColumnName("type");
            entity.Property(i => i.Quantity).HasColumnName("quantity");
            entity.Property(i => i.ReferenceId).HasColumnName("reference_id");
            entity.Property(i => i.CreatedAt).HasColumnName("created_at");
        });

        modelBuilder.Entity<SyncQueue>(entity =>
        {
            entity.ToTable("sync_queue");
            entity.Property(s => s.Id).HasColumnName("id");
            entity.Property(s => s.DeviceId).HasColumnName("device_id");
            entity.Property(s => s.Payload).HasColumnName("payload");
            entity.Property(s => s.Type).HasColumnName("type");
            entity.Property(s => s.Status).HasColumnName("status");
            entity.Property(s => s.RetryCount).HasColumnName("retry_count");
            entity.Property(s => s.CreatedAt).HasColumnName("created_at");
            entity.Property(s => s.SyncedAt).HasColumnName("synced_at");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("products");
            entity.Property(p => p.Id).HasColumnName("id");
            entity.Property(p => p.StoreId).HasColumnName("store_id");
            entity.Property(p => p.Name).HasColumnName("name");
            entity.Property(p => p.Price).HasColumnName("price");
            entity.Property(p => p.StockQuantity).HasColumnName("stock_quantity");
            entity.Property(p => p.IsActive).HasColumnName("is_active");
            entity.Property(p => p.CreatedAt).HasColumnName("created_at");
        });
    }
}
