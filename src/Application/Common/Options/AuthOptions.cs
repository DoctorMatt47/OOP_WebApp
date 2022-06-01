using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Application.Common.Options;

public static class AuthOptions
{
    public const string Issuer = "OOP_WebApp-AuthServer";
    public const string Audience = "OOP_WebApp-AuthClient";
    private const string Key = "OOP_WebApp-SecretKey";

    public static SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.ASCII.GetBytes(Key));
}
