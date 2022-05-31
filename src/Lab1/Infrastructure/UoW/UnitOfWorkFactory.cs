using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.UoW;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly string _connectionString;

    public UnitOfWorkFactory(IConfiguration configuration) =>
        _connectionString = configuration
            .GetSection("ConnectionStrings")
            .GetSection("OOP_WebApp").Value;

    public IUnitOfWork Create() => new UnitOfWork(_connectionString);
}
