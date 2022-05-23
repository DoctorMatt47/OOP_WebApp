using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record QuestionId(Guid Value) : Id<Guid>(Value);

public class Question : Entity<QuestionId>
{
    public Question(QuestionString s)
    {
        String = s;
        Id = new QuestionId(Guid.NewGuid());
    }

    public QuestionString String { get; }

    public TestId TestId { get; } = null!;
}