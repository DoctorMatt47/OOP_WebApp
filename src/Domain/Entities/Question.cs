using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record QuestionId(Guid Value) : Id<Guid>(Value);

public class Question : Entity<QuestionId>
{
    public Question(QuestionString s, TestId testId)
    {
        String = s;
        TestId = testId;
        Id = new QuestionId(Guid.NewGuid());
    }

    public QuestionString String { get; }

    public TestId TestId { get; }
}