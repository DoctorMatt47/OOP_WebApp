using Application.Common.Interfaces;
using Lab1.Infrastructure.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace Lab1.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services.AddTransient<IUnitOfWorkFactory, UnitOfWorkFactory>();
}