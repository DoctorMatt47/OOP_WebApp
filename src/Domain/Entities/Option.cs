using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record OptionId(Guid Value) : Id<Guid>(Value);

public class Option : Entity<OptionId>
{
    public Option(OptionString s) => String = s;

    public OptionString String { get; }

    // ReSharper disable ReplaceAutoPropertyWithComputedProperty
    public QuestionId QuestionId { get; } = null!;
    // ReSharper restore ReplaceAutoPropertyWithComputedProperty
}
