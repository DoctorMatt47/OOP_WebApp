using System.Data.Common;
using Application.Common.Interfaces;
using Infrastructure.Repositories;
using Npgsql;

namespace Infrastructure.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbConnection _connection;
    private readonly DbTransaction _transaction;

    public UnitOfWork(string connectionString)
    {
        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
        _transaction = _connection.BeginTransaction();

        Options = new OptionRepository(_connection);
        Questions = new QuestionRepository(_connection);
        Tests = new TestRepository(_connection);
    }

    public IOptionRepository Options { get; }
    public IQuestionRepository Questions { get; }
    public ITestRepository Tests { get; }

    public Task SaveChangesAsync() => _transaction.CommitAsync();

    public async ValueTask DisposeAsync()
    {
        await _transaction.DisposeAsync();
        await _connection.CloseAsync();
        await _connection.DisposeAsync();
    }
}
