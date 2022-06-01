using System.Text;
using System.Text.Json;
using OOP_WebApp.Application.Users;
using OOP_WebApp.Lab1.WebAPI.JsonConverters;

namespace OOP_WebApp.Lab1.WebAPI.Middlewares;

public class UsersMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IUserService _users;

    public UsersMiddleware(RequestDelegate next, IUserService users)
    {
        _next = next;
        _users = users;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLowerInvariant();

        if (path is null || !path.StartsWith("/api/users"))
        {
            await _next.Invoke(context);
            return;
        }

        var task = (path, context.Request.Method) switch
        {
            ("/api/users", "POST") => CreateUser(context),
            ("/api/users/authenticate", "POST") => Authenticate(context),
            _ => MethodNotAllowed(context)
        };
        await task;
    }

    public async Task CreateUser(HttpContext context)
    {
        var requestBody = await context.Request.BodyReader.ReadAsync();
        var testJson = Encoding.UTF8.GetString(requestBody.Buffer);
        var userRequest = JsonSerializer.Deserialize<CreateUserRequest>(testJson, CustomJsonOptions.Get());

        if (userRequest is null)
        {
            context.Response.StatusCode = 400;
            return;
        }

        var authResponse = await _users.Create(userRequest, context.RequestAborted);
        var authJson = JsonSerializer.Serialize(authResponse, CustomJsonOptions.Get());
        var authBytes = Encoding.UTF8.GetBytes(authJson);

        context.Response.StatusCode = 200;
        context.Response.ContentType = "application/json; charset=utf-8";
        await context.Response.Body.WriteAsync(authBytes, 0, authBytes.Length);
    }

    public async Task Authenticate(HttpContext context)
    {
        var requestBody = await context.Request.BodyReader.ReadAsync();
        var userJson = Encoding.UTF8.GetString(requestBody.Buffer);
        var userRequest = JsonSerializer.Deserialize<AuthenticateRequest>(userJson, CustomJsonOptions.Get());

        if (userRequest is null)
        {
            context.Response.StatusCode = 400;
            return;
        }

        var authResponse = await _users.Authenticate(userRequest, context.RequestAborted);
        var authJson = JsonSerializer.Serialize(authResponse, CustomJsonOptions.Get());
        var authBytes = Encoding.UTF8.GetBytes(authJson);

        context.Response.StatusCode = 200;
        context.Response.ContentType = "application/json; charset=utf-8";
        await context.Response.Body.WriteAsync(authBytes, 0, authBytes.Length);
    }

    public static Task MethodNotAllowed(HttpContext context)
    {
        context.Response.StatusCode = 405;
        return Task.CompletedTask;
    }
}