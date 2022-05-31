using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record UserId : Id<Guid, UserId>;

public class User : Entity<UserId>
{
    public User(UserId id, string username, string passwordHash) : base(id)
    {
        Username = username;
        PasswordHash = passwordHash;
    }

    public string Username { get; }
    public string PasswordHash { get; }
}
