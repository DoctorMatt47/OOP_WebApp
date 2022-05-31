using OOP_WebApp.Domain.ValueObjects;

namespace Application.Tests;

public record CreateQuestionRequest(QuestionString String, IEnumerable<CreateOptionRequest> Options);