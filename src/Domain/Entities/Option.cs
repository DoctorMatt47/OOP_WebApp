using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record OptionId(Guid Value) : Id<Guid>(Value);

public class Option : Entity<OptionId>
{
    
}
