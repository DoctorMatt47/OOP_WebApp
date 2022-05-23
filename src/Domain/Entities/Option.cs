using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record OptionId(Guid Value) : Id<Guid>(Value);

public class Option : Entity<OptionId>
{
    public Option(OptionString s, QuestionId questionId)
    {
        String = s;
        QuestionId = questionId;
        Id = new OptionId(Guid.NewGuid());
    }

    public OptionString String { get; }

    public QuestionId QuestionId { get; }
}
