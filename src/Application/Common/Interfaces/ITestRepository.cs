using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Application.Common.Interfaces;

public interface ITestRepository
{
    Task<IEnumerable<Test>> Get(CancellationToken cancellationToken);
    Task<Test?> Get(TestId id, CancellationToken cancellationToken);
    Task Create(Test test, CancellationToken cancellationToken);
    Task Delete(TestId id, CancellationToken cancellationToken);
}