using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CashflowGateway.Domain;

namespace CashflowGateway.Application;

public class InventoryService : IInventoryService
{
    private readonly IAppDbContext _context;

    public InventoryService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductResponseDto>> GetProductsAsync(Guid? storeId)
    {
        var query = _context.Products
            .Where(p => p.IsActive == true);

        if (storeId.HasValue)
            query = query.Where(p => p.StoreId == storeId.Value);

        return await query
            .OrderBy(p => p.Name)
            .Select(p => new ProductResponseDto
            {
                Id        = p.Id,
                StoreId   = p.StoreId,
                Name      = p.Name,
                Price     = p.Price,
                Stock     = p.StockQuantity,
                IsActive  = p.IsActive,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync();
    }


    public async Task<ProductResponseDto?> GetProductByIdAsync(Guid id)
    {
        var p = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        if (p == null) return null;

        return new ProductResponseDto
        {
            Id        = p.Id,
            StoreId   = p.StoreId,
            Name      = p.Name,
            Price     = p.Price,
            Stock     = p.StockQuantity,
            IsActive  = p.IsActive,
            CreatedAt = p.CreatedAt
        };
    }

    public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto request)
    {
        var product = new Product
        {
            Id            = Guid.NewGuid(),
            StoreId       = request.StoreId,
            Name          = request.Name,
            Price         = request.Price,
            StockQuantity = request.Stock,
            IsActive      = true,
            CreatedAt     = DateTime.UtcNow
        };
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        if (request.Stock > 0)
        {
            var movement = new InventoryMovement
            {
                Id        = Guid.NewGuid(),
                ProductId = product.Id,
                Type      = "RESTOCK",
                Quantity  = request.Stock,
                CreatedAt = DateTime.UtcNow
            };
            _context.InventoryMovements.Add(movement);
            await _context.SaveChangesAsync();
        }

        return new ProductResponseDto
        {
            Id        = product.Id,
            StoreId   = product.StoreId,
            Name      = product.Name,
            Price     = product.Price,
            Stock     = product.StockQuantity,
            IsActive  = product.IsActive,
            CreatedAt = product.CreatedAt
        };
    }


    public async Task<ProductResponseDto?> UpdateProductAsync(Guid id, UpdateProductDto request)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return null;

        var oldStock = product.StockQuantity;

        if (request.Name  != null) product.Name          = request.Name;
        if (request.Price != null) product.Price         = request.Price.Value;
        if (request.Stock != null) product.StockQuantity = request.Stock.Value;
        if (request.IsActive != null) product.IsActive   = request.IsActive.Value;

        await _context.SaveChangesAsync();

        if (request.Stock.HasValue && request.Stock.Value != oldStock)
        {
            var difference = request.Stock.Value - oldStock;
            var movement = new InventoryMovement
            {
                Id        = Guid.NewGuid(),
                ProductId = product.Id,
                Type      = "ADJUSTMENT",
                Quantity  = difference,  
                CreatedAt = DateTime.UtcNow
            };
            _context.InventoryMovements.Add(movement);
            await _context.SaveChangesAsync();
        }

        return new ProductResponseDto
        {
            Id        = product.Id,
            StoreId   = product.StoreId,
            Name      = product.Name,
            Price     = product.Price,
            Stock     = product.StockQuantity,
            IsActive  = product.IsActive,
            CreatedAt = product.CreatedAt
        };
    }


    public async Task<bool> DeactivateProductAsync(Guid id)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null) return false;

        product.IsActive = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<InventoryMovementResponseDto>> GetMovementsAsync(Guid productId)
    {
        return await _context.InventoryMovements
            .Where(m => m.ProductId == productId)
            .OrderByDescending(m => m.CreatedAt)
            .Select(m => new InventoryMovementResponseDto
            {
                Id          = m.Id,
                ProductId   = m.ProductId,
                Type        = m.Type,
                Quantity    = m.Quantity,
                ReferenceId = m.ReferenceId,
                CreatedAt   = m.CreatedAt
            })
            .ToListAsync();
    }
}