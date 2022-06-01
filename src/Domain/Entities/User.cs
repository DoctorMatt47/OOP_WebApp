using System.Text.RegularExpressions;
using OOP_WebApp.Domain.EntityEnums;
using OOP_WebApp.Domain.Exceptions;
using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record Username : Id<string, Username>
{
    private static readonly Regex Regex = new(@"[A-Za-z0-9\.,;:!?()""'%\- ]{2,30}");

    protected override void Validate()
    {
        if (!Regex.IsMatch(Value)) throw new DomainArgumentException("String is not matching regex", nameof(Value));
    }
}

public class User : Entity<Username>
{
    protected User()
    {
    }

    public User(Username username, Role role, string passwordHash, string passwordSalt) : base(username)
    {
        Role = role;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }

    public Role Role { get; protected set; }
    public string PasswordHash { get; protected set; } = null!;
    public string PasswordSalt { get; protected set; } = null!;
}