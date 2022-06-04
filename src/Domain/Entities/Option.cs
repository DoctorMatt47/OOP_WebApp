using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record OptionId : Id<Guid, OptionId>;

public class Option : Entity<OptionId>
{
    protected Option()
    {
    }

    public Option(OptionId id, OptionString @string, QuestionId questionId) : base(id)
    {
        String = @string;
        QuestionId = questionId;
    }

    public OptionString String { get; protected set; } = null!;

    public QuestionId QuestionId { get; protected set; } = null!;
}