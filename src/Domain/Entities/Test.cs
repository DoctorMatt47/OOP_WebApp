using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record TestId : Id<Guid, TestId>;

public class Test : Entity<TestId>
{
    protected Test()
    {
    }

    public Test(TestId id, TestTitleString title, TestDescriptionString description, Username userId) : base(id)
    {
        Title = title;
        Description = description;
        UserId = userId;
    }

    public TestTitleString Title { get; protected set; } = null!;
    public TestDescriptionString Description { get; protected set; } = null!;

    public Username UserId { get; protected set; } = null!;
}