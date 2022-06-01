using Application.Common.Interfaces;
using Lab2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using OOP_WebApp.Domain.Entities;

namespace Lab2.Infrastructure.Repositories;

public class TestRepository : RepositoryBase, ITestRepository
{
    public TestRepository(OopWebAppContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Test>> Get(Username id, CancellationToken cancellationToken) =>
        await Context.Tests
            .Where(t => t.UserId == id)
            .ToListAsync(cancellationToken);

    public async Task<Test?> Get(TestId id, CancellationToken cancellationToken) => await Context.Tests.FindAsync(id);

    public Task Create(Test test, CancellationToken cancellationToken)
    {
        Context.Add(test);
        return Task.CompletedTask;
    }

    public async Task Delete(TestId id, CancellationToken cancellationToken)
    {
        var test = await Context.Tests.FindAsync(id);
        if (test is not null) Context.Remove(test);
    }
}