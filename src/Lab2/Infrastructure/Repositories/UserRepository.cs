using Microsoft.EntityFrameworkCore;
using OOP_WebApp.Application.Common.Interfaces;
using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Lab2.Infrastructure.Persistence;

namespace OOP_WebApp.Lab2.Infrastructure.Repositories;

public class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(OopWebAppContext context) : base(context)
    {
    }

    public async Task<User?> Get(Username id, CancellationToken cancellationToken) => await Context.Users.FindAsync(id);

    public async Task<IEnumerable<User>> Get(TestId id, CancellationToken cancellationToken) =>
        await Context.Answers
            .Where(a => a.TestId == id)
            .Select(a => a.Username)
            .Distinct()
            .Join(Context.Users, un => un, u => u.Id, (un, u) => u)
            .ToListAsync(cancellationToken);

    public Task Create(User user, CancellationToken cancellationToken)
    {
        Context.Users.Add(user);
        return Task.CompletedTask;
    }
}
