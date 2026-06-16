using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CashflowGateway.Domain;

namespace CashflowGateway.Application;

public class DeviceService : IDeviceService
{
    private readonly IAppDbContext _context;

    public DeviceService(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<DeviceResponseDto> RegisterAsync(RegisterDeviceDto request)
    {
        var device = new Device
        {
            Id         = Guid.NewGuid(),
            StoreId    = request.StoreId,
            DeviceName = request.DeviceName,
            DeviceKey  = request.DeviceKey,
            Status     = "OFFLINE",
            CreatedAt  = DateTime.UtcNow
        };

        _context.Devices.Add(device);
        await _context.SaveChangesAsync();

        return MapToDto(device);
    }

    public async Task<List<DeviceResponseDto>> GetAllAsync()
    {
        return await _context.Devices
            .OrderByDescending(d => d.LastSeen)
            .Select(d => new DeviceResponseDto
            {
                Id         = d.Id,
                StoreId    = d.StoreId,
                DeviceName = d.DeviceName,
                DeviceKey  = d.DeviceKey,
                Status     = d.Status,
                LastSeen   = d.LastSeen,
                CreatedAt  = d.CreatedAt
            })
            .ToListAsync();
    }

 
    public async Task<DeviceResponseDto?> PingAsync(Guid deviceId)
    {
        var device = await _context.Devices
            .FirstOrDefaultAsync(d => d.Id == deviceId);

        if (device == null) return null;

        device.LastSeen = DateTime.UtcNow;
        device.Status   = "ONLINE";

        await _context.SaveChangesAsync();

        return MapToDto(device);
    }

    public async Task<bool> DeactivateAsync(Guid deviceId)
    {
        var device = await _context.Devices
            .FirstOrDefaultAsync(d => d.Id == deviceId);

        if (device == null) return false;

        device.Status = "OFFLINE";
        await _context.SaveChangesAsync();
        return true;
    }

    private static DeviceResponseDto MapToDto(Device d) => new()
    {
        Id         = d.Id,
        StoreId    = d.StoreId,
        DeviceName = d.DeviceName,
        DeviceKey  = d.DeviceKey,
        Status     = d.Status,
        LastSeen   = d.LastSeen,
        CreatedAt  = d.CreatedAt
    };
}