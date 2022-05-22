using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record TestId(Guid Value) : Id<Guid>(Value);

public class Test : Entity<TestId>
{
    
}
