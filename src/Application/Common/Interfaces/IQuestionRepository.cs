using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Application.Common.Interfaces;

public interface IQuestionRepository
{
    Task<IEnumerable<Question>> Get(TestId id, CancellationToken cancellationToken);
    Task Create(IEnumerable<Question> questions, CancellationToken cancellationToken);
}