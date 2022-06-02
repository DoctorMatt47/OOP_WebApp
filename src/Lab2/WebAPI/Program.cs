using OOP_WebApp.Application.Common.Extensions;
using OOP_WebApp.Lab2.Infrastructure.Extensions;
using OOP_WebApp.Lab2.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonConverters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddBearerAuthentication();

const string origins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
    options.AddPolicy(origins, policy => policy.WithOrigins("http://localhost:4200")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseCors(origins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();