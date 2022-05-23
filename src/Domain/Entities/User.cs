using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record UserId(Guid Value) : Id<Guid>(Value);

public class User : Entity<UserId>
{
    public User(string username, string passwordHash)
    {
        Username = username;
        PasswordHash = passwordHash;
    }

    public string Username { get; }
    public string PasswordHash { get; }
}
