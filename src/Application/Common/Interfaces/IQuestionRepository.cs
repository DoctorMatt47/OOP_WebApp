using OOP_WebApp.Domain.Entities;

namespace Application.Common.Interfaces;

public interface IQuestionRepository
{
    Task<IEnumerable<Question>> Get(TestId id, CancellationToken cancellationToken);
    Task Create(IEnumerable<Option> options, CancellationToken cancellationToken);
}