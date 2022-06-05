using Microsoft.Extensions.DependencyInjection;
using OOP_WebApp.Application.Answers;
using OOP_WebApp.Application.Identity;
using OOP_WebApp.Application.Tests;
using OOP_WebApp.Application.Users;

namespace OOP_WebApp.Application.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services
            .AddTransient<IJwtTokenService, JwtTokenService>()
            .AddTransient<IHashService, HashService>()
            .AddTransient<ITestService, TestService>()
            .AddTransient<IUserService, UserService>()
            .AddTransient<IAnswerService, AnswerService>();
}