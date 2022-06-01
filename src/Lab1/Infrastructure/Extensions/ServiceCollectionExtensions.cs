using Microsoft.Extensions.DependencyInjection;
using OOP_WebApp.Application.Common.Interfaces;
using OOP_WebApp.Lab1.Infrastructure.UoW;

namespace OOP_WebApp.Lab1.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();
}