using Microsoft.Extensions.Configuration;
using Npgsql;
using OOP_WebApp.Application.Common.Interfaces;
using OOP_WebApp.Lab1.Infrastructure.Repositories;

namespace OOP_WebApp.Lab1.Infrastructure.UoW;

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
        var answers = new AnswerRepository(connection);

        return new UnitOfWork(connection, options, questions, tests, users, answers);
    }
}