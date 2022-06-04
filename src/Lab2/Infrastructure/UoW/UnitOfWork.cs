using OOP_WebApp.Application.Common.Interfaces;
using OOP_WebApp.Lab2.Infrastructure.Persistence;

namespace OOP_WebApp.Lab2.Infrastructure.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly OopWebAppContext _context;

    public UnitOfWork(
        OopWebAppContext context,
        IOptionRepository options,
        IQuestionRepository questions,
        ITestRepository tests,
        IUserRepository users,
        IAnswerRepository answers)
    {
        _context = context;
        Options = options;
        Questions = questions;
        Tests = tests;
        Users = users;
        Answers = answers;
    }

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;

    public IOptionRepository Options { get; }
    public IQuestionRepository Questions { get; }
    public ITestRepository Tests { get; }
    public IUserRepository Users { get; }
    public IAnswerRepository Answers { get; }

    public Task SaveChangesAsync() => _context.SaveChangesAsync();
}