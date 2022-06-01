using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OOP_WebApp.Application.Common.Interfaces;
using OOP_WebApp.Lab2.Infrastructure.Persistence;
using OOP_WebApp.Lab2.Infrastructure.UoW;

namespace OOP_WebApp.Lab2.Infrastructure.Extensions;

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