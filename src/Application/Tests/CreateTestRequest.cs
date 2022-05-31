using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.ValueObjects;

namespace Application.Tests;

public record CreateTestRequest(
    TestTitleString Title,
    TestDescriptionString Description,
    UserId UserId,
    IEnumerable<CreateQuestionRequest> Questions);