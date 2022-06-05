using OOP_WebApp.Application.Common.Extensions;
using OOP_WebApp.Lab1.Infrastructure.Extensions;
using OOP_WebApp.Lab1.WebAPI.Extensions;
using OOP_WebApp.Lab1.WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonConverters();

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddBearerAuthentication();

const string origins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
    options.AddPolicy(origins, policy => policy.WithOrigins("http://localhost:4200")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(origins);

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ErrorsMiddleware>();
app.UseMiddleware<TestsMiddleware>();
app.UseMiddleware<UsersMiddleware>();

app.MapControllers();

app.Run();