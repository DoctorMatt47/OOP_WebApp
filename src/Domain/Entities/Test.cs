using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record TestId(Guid Value) : Id<Guid>(Value);

public class Test : Entity<TestId>
{
    public Test(TestTitleString title, TestDescriptionString description, UserId userId)
    {
        Title = title;
        Description = description;
        UserId = userId;
        Id = new TestId(Guid.NewGuid());
    }

    public TestTitleString Title { get; }
    public TestDescriptionString Description { get; }

    public UserId UserId { get; }
}