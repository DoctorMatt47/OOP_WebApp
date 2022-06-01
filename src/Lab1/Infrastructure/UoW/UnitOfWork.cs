using System.Data.Common;
using Application.Common.Interfaces;

namespace Infrastructure.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbConnection _connection;
    private readonly DbTransaction _transaction;

    public UnitOfWork(
        DbConnection connection,
        IOptionRepository options,
        IQuestionRepository questions,
        ITestRepository tests,
        IUserRepository users)
    {
        _connection = connection;
        Options = options;
        Questions = questions;
        Tests = tests;
        Users = users;

        _connection.Open();
        _transaction = _connection.BeginTransaction();
    }

    public IOptionRepository Options { get; }
    public IQuestionRepository Questions { get; }
    public ITestRepository Tests { get; }
    public IUserRepository Users { get; }

    public Task SaveChangesAsync() => _transaction.CommitAsync();

    public async ValueTask DisposeAsync()
    {
        await _transaction.DisposeAsync();
        await _connection.CloseAsync();
        await _connection.DisposeAsync();
    }
}