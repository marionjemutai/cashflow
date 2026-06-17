using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CashflowGateway.Domain;

namespace CashflowGateway.Application;

public class StoreService : IStoreService
{
    private readonly IAppDbContext _context;

    public StoreService(IAppDbContext context)
    {
        _context = context;
    }


    public async Task<List<StoreResponseDto>> GetAllAsync()
    {
        return await _context.Stores
            .OrderBy(s => s.Name)
            .Select(s => new StoreResponseDto
            {
                Id        = s.Id,
                Name      = s.Name,
                Location  = s.Location,
                CreatedAt = s.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<StoreResponseDto?> GetByIdAsync(Guid id)
    {
        var store = await _context.Stores
            .FirstOrDefaultAsync(s => s.Id == id);

        if (store == null) return null;

        return new StoreResponseDto
        {
            Id        = store.Id,
            Name      = store.Name,
            Location  = store.Location,
            CreatedAt = store.CreatedAt
        };
    }

    public async Task<StoreResponseDto> CreateAsync(CreateStoreDto request)
    {
        var store = new Store
        {
            Id        = Guid.NewGuid(),
            Name      = request.Name,
            Location  = request.Location,
            CreatedAt = DateTime.UtcNow
        };

        _context.Stores.Add(store);
        await _context.SaveChangesAsync();

        return new StoreResponseDto
        {
            Id        = store.Id,
            Name      = store.Name,
            Location  = store.Location,
            CreatedAt = store.CreatedAt
        };
    }

  
    public async Task<StoreResponseDto?> UpdateAsync(Guid id, UpdateStoreDto request)
    {
        var store = await _context.Stores
            .FirstOrDefaultAsync(s => s.Id == id);

        if (store == null) return null;

        if (request.Name     != null) store.Name     = request.Name;
        if (request.Location != null) store.Location = request.Location;

        await _context.SaveChangesAsync();

        return new StoreResponseDto
        {
            Id        = store.Id,
            Name      = store.Name,
            Location  = store.Location,
            CreatedAt = store.CreatedAt
        };
    }
}