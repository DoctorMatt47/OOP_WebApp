using Application.Common.Responses;
using OOP_WebApp.Domain.Entities;

namespace Application.Tests;

public interface ITestService
{
    Task<GetTestResponse> Get(TestId id, CancellationToken cancellationToken);
    Task<GuidIdResponse> Create(CreateTestRequest request, UserId userId, CancellationToken cancellationToken);
    Task Delete(TestId id, CancellationToken cancellationToken);
}
