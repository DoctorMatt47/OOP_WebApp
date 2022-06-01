using Application.Common.Interfaces;
using Lab1.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Lab1.Infrastructure.UoW;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly string _connectionString;

    public UnitOfWorkFactory(IConfiguration configuration) =>
        _connectionString = configuration
            .GetSection("ConnectionStrings")
            .GetSection("OOP_WebApp").Value;

    public IUnitOfWork Create()
    {
        var connection = new NpgsqlConnection(_connectionString);
        var options = new OptionRepository(connection);
        var questions = new QuestionRepository(connection);
        var tests = new TestRepository(connection);
        var users = new UserRepository(connection);

        return new UnitOfWork(connection, options, questions, tests, users);
    }
}
