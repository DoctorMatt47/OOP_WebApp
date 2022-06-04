using System.Data.Common;
using OOP_WebApp.Application.Common.Interfaces;

namespace OOP_WebApp.Lab1.Infrastructure.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbConnection _connection;
    private DbTransaction _transaction;

    public UnitOfWork(
        DbConnection connection,
        IOptionRepository options,
        IQuestionRepository questions,
        ITestRepository tests,
        IUserRepository users,
        IAnswerRepository answers)
    {
        _connection = connection;
        Options = options;
        Questions = questions;
        Tests = tests;
        Users = users;
        Answers = answers;

        _connection.Open();
        _transaction = _connection.BeginTransaction();
    }

    public IOptionRepository Options { get; }
    public IQuestionRepository Questions { get; }
    public ITestRepository Tests { get; }
    public IUserRepository Users { get; }
    public IAnswerRepository Answers { get; }

    public async Task SaveChangesAsync()
    {
        await _transaction.CommitAsync();
        _transaction = await _connection.BeginTransactionAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _transaction.DisposeAsync();
        await _connection.CloseAsync();
        await _connection.DisposeAsync();
    }
}