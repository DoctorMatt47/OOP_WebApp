using OOP_WebApp.Domain.ValueObjects;

namespace Application.Tests;

public record CreateTestRequest(
    TestTitleString Title,
    TestDescriptionString Description,
    IEnumerable<CreateQuestionRequest> Questions);