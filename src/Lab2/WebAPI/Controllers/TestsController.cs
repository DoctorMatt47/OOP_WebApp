﻿using Application.Common.Responses;
using Application.Tests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OOP_WebApp.Domain.Entities;

namespace Lab2.WebAPI.Controllers;

public class TestsController : ApiControllerBase
{
    private readonly ITestService _tests;

    public TestsController(ITestService tests) => _tests = tests;

    [HttpGet]
    [Authorize]
    public Task<GetTestResponse> Get(Guid id, CancellationToken cancellationToken) =>
        _tests.Get(TestId.From(id), cancellationToken);

    [HttpPost]
    [Authorize(Roles = "Tutor")]
    public Task<GuidIdResponse> Create(CreateTestRequest request, CancellationToken cancellationToken) =>
        _tests.Create(request, Username.From(User.Identity?.Name!), cancellationToken);
}