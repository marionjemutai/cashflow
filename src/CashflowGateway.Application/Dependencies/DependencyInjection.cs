using CashflowGateway.Application;
using Microsoft.Extensions.DependencyInjection;
namespace CashflowGateway.Application.Dependencies;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ISyncService, SyncService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IInventoryService, InventoryService>();
        services.AddScoped<ILedgerService, LedgerService>();
        services.AddScoped<IDeviceService, DeviceService>();
        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<IReceiptService, ReceiptService>();
        services.AddScoped<IReportService, ReportService>();
        return services;
    }
}