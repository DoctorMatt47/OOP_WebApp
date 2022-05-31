using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record OptionId : Id<Guid, OptionId>;

public class Option : Entity<OptionId>
{
    public Option(OptionId id, OptionString s, QuestionId questionId) : base(id)
    {
        String = s;
        QuestionId = questionId;
    }

    public OptionString String { get; }

    public QuestionId QuestionId { get; }
}
