using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record QuestionId(Guid Value) : Id<Guid>(Value);

public class Question : Entity<QuestionId>
{
    public Question(QuestionString s) => String = s;

    public QuestionString String { get; }

    private List<Option> _options = new();

    public IEnumerable<Option> Options => _options.ToList();
}
