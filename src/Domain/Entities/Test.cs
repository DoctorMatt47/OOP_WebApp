using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record TestId : Id<Guid, TestId>;

public class Test : Entity<TestId>
{
    public Test(TestId id, TestTitleString title, TestDescriptionString description, Username userId) : base(id)
    {
        Title = title;
        Description = description;
        UserId = userId;
    }

    public TestTitleString Title { get; }
    public TestDescriptionString Description { get; }

    public Username UserId { get; }
}