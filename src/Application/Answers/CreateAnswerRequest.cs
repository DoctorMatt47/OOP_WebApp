using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Application.Answers;

public record CreateAnswerRequest(Username Username, TestId TestId, QuestionId QuestionId, OptionId OptionId);
