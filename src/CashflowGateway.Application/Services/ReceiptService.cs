using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CashflowGateway.Application;

public class ReceiptService : IReceiptService
{
    private readonly IAppDbContext _context;

    public ReceiptService(IAppDbContext context)
    {
        _context = context;
    }
    public async Task<ReceiptDto?> GetReceiptAsync(Guid transactionId)
    {
        var transaction = await _context.Transactions
            .FirstOrDefaultAsync(t => t.Id == transactionId);

        if (transaction == null) return null;
        var store = await _context.Stores
            .FirstOrDefaultAsync(s => s.Id == transaction.StoreId);
        var items = await _context.TransactionItems
            .Where(ti => ti.TransactionId == transactionId)
            .ToListAsync();

        var receiptItems = new System.Collections.Generic.List<ReceiptItemDto>();

        foreach (var item in items)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == item.ProductId);

            receiptItems.Add(new ReceiptItemDto
            {
                ProductName = product?.Name ?? "Unknown Product",
                Quantity    = item.Quantity,
                UnitPrice   = item.UnitPrice,
                Subtotal    = item.Quantity * item.UnitPrice
            });
        }

        return new ReceiptDto
        {
            TransactionId = transaction.Id,
            StoreName     = store?.Name ?? "Unknown Store",
            StoreLocation = store?.Location,
            Status        = transaction.Status,
            CreatedAt     = transaction.CreatedAt,
            TotalAmount   = transaction.TotalAmount,
            Items         = receiptItems
        };
    }
}