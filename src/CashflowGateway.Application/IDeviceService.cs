using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CashflowGateway.Application;

public interface IDeviceService
{
    Task<DeviceResponseDto>        RegisterAsync(RegisterDeviceDto request);
    Task<List<DeviceResponseDto>>  GetAllAsync();
    Task<DeviceResponseDto?>       PingAsync(Guid deviceId);
    Task<bool>                     DeactivateAsync(Guid deviceId);
}