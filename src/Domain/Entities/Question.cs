using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record QuestionId : Id<Guid, QuestionId>;

public class Question : Entity<QuestionId>
{
    public Question()
    {
    }

    public Question(QuestionId id, QuestionString @string, TestId testId) : base(id)
    {
        String = @string;
        TestId = testId;
    }

    public QuestionString String { get; protected set; } = null!;

    public TestId TestId { get; protected set; } = null!;
}