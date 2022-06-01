using Microsoft.EntityFrameworkCore;
using OOP_WebApp.Application.Common.Interfaces;
using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Lab2.Infrastructure.Persistence;

namespace OOP_WebApp.Lab2.Infrastructure.Repositories;

public class OptionRepository : RepositoryBase, IOptionRepository
{
    public OptionRepository(OopWebAppContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Option>> Get(QuestionId id, CancellationToken cancellationToken) =>
        await Context.Options
            .Where(o => o.QuestionId == id)
            .ToListAsync(cancellationToken);

    public Task Create(IEnumerable<Option> options, CancellationToken cancellationToken)
    {
        Context.Options.AddRange(options);
        return Task.CompletedTask;
    }
}