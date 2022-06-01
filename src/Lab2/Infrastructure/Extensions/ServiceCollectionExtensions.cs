using Application.Common.Interfaces;
using Lab2.Infrastructure.Persistence;
using Lab2.Infrastructure.UoW;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lab2.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration
            .GetSection("ConnectionStrings")
            .GetSection("OOP_WebApp").Value;

        services.AddNpgsql<OopWebAppContext>(connectionString);
        services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();

        return services;
    }
}