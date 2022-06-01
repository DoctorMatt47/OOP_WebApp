using Application.Common.Interfaces;
using Infrastructure.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
}