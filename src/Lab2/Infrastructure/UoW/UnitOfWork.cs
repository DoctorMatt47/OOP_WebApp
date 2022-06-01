using Application.Common.Interfaces;
using Lab2.Infrastructure.Persistence;

namespace Lab2.Infrastructure.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly OopWebAppContext _context;

    public UnitOfWork(
        OopWebAppContext context,
        IOptionRepository options,
        IQuestionRepository questions,
        ITestRepository tests,
        IUserRepository users)
    {
        _context = context;
        Options = options;
        Questions = questions;
        Tests = tests;
        Users = users;
    }

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

    public IOptionRepository Options { get; }
    public IQuestionRepository Questions { get; }
    public ITestRepository Tests { get; }
    public IUserRepository Users { get; }

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}
