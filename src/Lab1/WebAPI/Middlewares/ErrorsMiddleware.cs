using OOP_WebApp.Application.Common.Exceptions;

namespace OOP_WebApp.Lab1.WebAPI.Middlewares;

public class ErrorsMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorsMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (NotFoundException e)
        {
            context.Response.StatusCode = 404;
        }
        catch (ConflictException e)
        {
            context.Response.StatusCode = 409;
        }
        catch (BadRequestException e)
        {
            context.Response.StatusCode = 400;
        }
        catch (Exception e)
        {
            context.Response.StatusCode = 500;
            throw;
        }
    }
}