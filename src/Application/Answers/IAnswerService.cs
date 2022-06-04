using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Application.Answers;

public interface IAnswerService
{
    Task<IEnumerable<GetAnswerResponse>> Get(TestId id, Username username, CancellationToken cancellationToken);
}