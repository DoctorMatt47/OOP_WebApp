using Application.Common.Interfaces;
using Lab2.Infrastructure.Persistence;
using Lab2.Infrastructure.Repositories;

namespace Lab2.Infrastructure.UoW;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly OopWebAppContext _context;

    public UnitOfWorkFactory(OopWebAppContext context) => _context = context;

    public IUnitOfWork Create()
    {
        var options = new OptionRepository(_context);
        var questions = new QuestionRepository(_context);
        var tests = new TestRepository(_context);
        var users = new UserRepository(_context);

        return new UnitOfWork(_context, options, questions, tests, users);
    }
}