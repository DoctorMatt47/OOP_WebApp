using Microsoft.EntityFrameworkCore;
using OOP_WebApp.Application.Common.Interfaces;
using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Lab2.Infrastructure.Persistence;

namespace OOP_WebApp.Lab2.Infrastructure.Repositories;

public class AnswerRepository : RepositoryBase, IAnswerRepository
{
    public AnswerRepository(OopWebAppContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Answer>> Get(TestId id, Username username, CancellationToken cancellationToken) =>
        await Context.Answers
            .Where(a => a.TestId == id && a.Username == username)
            .ToListAsync(cancellationToken);

    public async Task Create(IEnumerable<Answer> answers, CancellationToken cancellationToken) => 
        Context.AddRange(answers);
}
