using CashflowGateway.Application;
using CashflowGateway.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace CashflowGateway.Infrastructure.Dependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services)
    {

        services.AddScoped<IAppDbContext>(
            provider => provider.GetRequiredService<AppDbContext>());

        return services;
    }
}