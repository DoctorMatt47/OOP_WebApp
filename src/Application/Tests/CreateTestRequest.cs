using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Application.Tests;

public record CreateTestRequest(
    TestTitleString Title,
    TestDescriptionString Description,
    IEnumerable<CreateQuestionRequest> Questions);