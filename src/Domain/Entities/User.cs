using System.Text.RegularExpressions;
using OOP_WebApp.Domain.EntityEnums;
using OOP_WebApp.Domain.Exceptions;
using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Domain.Entities;

public record Username : Id<string, Username>
{
    private static readonly Regex Regex = new(@"[A-Za-z0-9\.,;:!?()""'%\- ]{2,25}");

    protected override void Validate()
    {
        if (!Regex.IsMatch(Value)) throw new DomainArgumentException("String is not matching regex", nameof(Value));
    }
}

public class User : Entity<Username>
{
    public User(Username username, string passwordHash, Role role) : base(username)
    {
        PasswordHash = passwordHash;
        Role = role;
    }

    public string PasswordHash { get; }
    public Role Role { get; }
}