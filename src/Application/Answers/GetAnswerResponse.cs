using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Application.Answers;

public record GetAnswerResponse(
    AnswerId Id,
    TestId TestId,
    QuestionId QuestionId,
    OptionId OptionId,
    Username Username);