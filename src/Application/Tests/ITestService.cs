using OOP_WebApp.Application.Common.Responses;
using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Application.Tests;

public interface ITestService
{
    Task<GetTestResponse> Get(TestId id, CancellationToken cancellationToken);
    Task<GuidIdResponse> Create(CreateTestRequest request, Username userId, CancellationToken cancellationToken);
    Task Delete(TestId id, CancellationToken cancellationToken);
}