using System;

namespace CashflowGateway.Application;


public class RegisterDeviceDto
{
    public Guid?  StoreId    { get; set; }
    public string DeviceName { get; set; } = string.Empty;
    public string DeviceKey  { get; set; } = string.Empty;
}


public class DeviceResponseDto
{
    public Guid      Id         { get; set; }
    public Guid?     StoreId    { get; set; }
    public string    DeviceName { get; set; } = string.Empty;
    public string    DeviceKey  { get; set; } = string.Empty;
    public string    Status     { get; set; } = string.Empty;
    public DateTime? LastSeen   { get; set; }
    public DateTime? CreatedAt  { get; set; }
}