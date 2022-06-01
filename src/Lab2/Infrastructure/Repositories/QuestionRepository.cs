using Application.Common.Interfaces;
using Lab2.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using OOP_WebApp.Domain.Entities;

namespace Lab2.Infrastructure.Repositories;

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