using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record QuestionId(Guid Value) : Id<Guid>(Value);

public class Question : Entity<QuestionId>
{
    
}
