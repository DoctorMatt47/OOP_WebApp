using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Application.Common.Interfaces;

public interface IAnswerRepository
{
    Task<IEnumerable<Answer>> Get(TestId id, Username username, CancellationToken cancellationToken);
    Task Create(IEnumerable<Answer> answers, CancellationToken cancellationToken);
}
