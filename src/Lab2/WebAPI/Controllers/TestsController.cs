using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OOP_WebApp.Application.Common.Responses;
using OOP_WebApp.Application.Tests;
using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Lab2.WebAPI.Controllers;

public class TestsController : ApiControllerBase
{
    private readonly ITestService _tests;

    public TestsController(ITestService tests) => _tests = tests;

    [HttpGet]
    [Authorize]
    public async Task<IEnumerable<GetTestResponse>> Get(CancellationToken cancellationToken) =>
        await _tests.Get(cancellationToken);

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<GetTestResponse> Get(Guid id, CancellationToken cancellationToken) =>
        await _tests.Get(TestId.From(id), cancellationToken);

    [HttpPost]
    [Authorize(Roles = "Tutor")]
    public Task<GuidIdResponse> Create(CreateTestRequest request, CancellationToken cancellationToken) =>
        _tests.Create(request, Username.From(User.Identity?.Name!), cancellationToken);
}
