using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Application.Tests;

public record GetQuestionResponse(QuestionId Id, QuestionString String, IEnumerable<GetOptionResponse> Options);