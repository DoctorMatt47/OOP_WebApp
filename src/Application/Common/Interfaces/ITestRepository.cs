using OOP_WebApp.Domain.Entities;

namespace Application.Common.Interfaces;

public interface ITestRepository
{
    Task<IEnumerable<Test>> Get(UserId id, CancellationToken cancellationToken);
    Task Create(Test test, CancellationToken cancellationToken);
    Task Delete(TestId id, CancellationToken cancellationToken);
}