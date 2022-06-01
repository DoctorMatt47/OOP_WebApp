using OOP_WebApp.Application.Common.Extensions;
using OOP_WebApp.Lab1.Infrastructure.Extensions;
using OOP_WebApp.Lab1.WebAPI.Extensions;
using OOP_WebApp.Lab1.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonConverters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddBearerAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorsMiddleware>();
app.UseMiddleware<TestsMiddleware>();
app.UseMiddleware<UsersMiddleware>();

app.MapControllers();

app.Run();