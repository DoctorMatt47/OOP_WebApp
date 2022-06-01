using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Application.Tests;

public record CreateQuestionRequest(QuestionString String, IEnumerable<CreateOptionRequest> Options);