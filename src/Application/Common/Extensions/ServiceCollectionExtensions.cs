using Application.Identity;
using Application.Tests;
using Application.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services
            .AddTransient<IJwtTokenService, JwtTokenService>()
            .AddTransient<IHashService, HashService>()
            .AddTransient<ITestService, TestService>()
            .AddTransient<IUserService, UserService>();
}
