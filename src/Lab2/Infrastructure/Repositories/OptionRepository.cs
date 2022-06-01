using Application.Common.Interfaces;
using Lab2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using OOP_WebApp.Domain.Entities;

namespace Lab2.Infrastructure.Repositories;

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
