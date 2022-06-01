using Microsoft.EntityFrameworkCore;
using OOP_WebApp.Application.Common.Interfaces;
using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Lab2.Infrastructure.Persistence;

namespace OOP_WebApp.Lab2.Infrastructure.Repositories;

public class QuestionRepository : RepositoryBase, IQuestionRepository
{
    public QuestionRepository(OopWebAppContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Question>> Get(TestId id, CancellationToken cancellationToken) =>
        await Context.Questions
            .Where(q => q.TestId == id)
            .ToListAsync(cancellationToken);

    public Task Create(IEnumerable<Question> questions, CancellationToken cancellationToken)
    {
        Context.Questions.AddRange(questions);
        return Task.CompletedTask;
    }
}