using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record AnswerId : Id<Guid, AnswerId>;

public class Answer : Entity<AnswerId>
{
    protected Answer()
    {
    }

    public Answer(AnswerId id, Username username, TestId testId, QuestionId questionId, OptionId optionId) : base(id)
    {
        Username = username;
        TestId = testId;
        QuestionId = questionId;
        OptionId = optionId;
    }

    public Username Username { get; protected set; } = null!;
    public TestId TestId { get; protected set; } = null!;
    public QuestionId QuestionId { get; protected set; } = null!;
    public OptionId OptionId { get; protected set; } = null!;
}