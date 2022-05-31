using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record QuestionId : Id<Guid, QuestionId>;

public class Question : Entity<QuestionId>
{
    public Question(QuestionId id, QuestionString @string, TestId testId) : base(id)
    {
        String = @string;
        TestId = testId;
    }

    public QuestionString String { get; }

    public TestId TestId { get; }
}
