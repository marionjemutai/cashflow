using CashflowGateway.Application.Dependencies;
using CashflowGateway.Infrastructure.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace CashflowGateway.API.Dependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddAppDI(this IServiceCollection services)
    {
        services.AddAppDbContext();
        services.AddApplicationServices();

        return services;
    }
}