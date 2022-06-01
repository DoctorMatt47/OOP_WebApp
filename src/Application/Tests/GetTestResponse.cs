using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Application.Tests;

public record GetTestResponse(
    TestId Id,
    TestTitleString Title,
    TestDescriptionString Description,
    Username UserId,
    IEnumerable<GetQuestionResponse> Questions);