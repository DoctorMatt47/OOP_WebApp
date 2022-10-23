using OOP_WebApp.Application.Common.Extensions;
using OOP_WebApp.Lab2.Infrastructure.Extensions;
using OOP_WebApp.Lab2.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCors()
    .AddControllers()
    .AddJsonConverters();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddBearerAuthentication()
    .AddEndpointsApiExplorer()
    .AddSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler("/error");

app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
