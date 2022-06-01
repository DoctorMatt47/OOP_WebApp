using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Common.Options;
using Microsoft.IdentityModel.Tokens;
using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.EntityEnums;

namespace Application.Identity;

public class JwtTokenService : IJwtTokenService
{
    public string Get(Username username, Role role)
    {
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, username.Value),
            new(ClaimsIdentity.DefaultRoleClaimType, Enum.GetName(role)!)
        };

        var identity = new ClaimsIdentity(
            claims,
            "Token",
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);

        var jwt = new JwtSecurityToken(
            AuthOptions.Issuer,
            AuthOptions.Audience,
            identity.Claims,
            DateTime.UtcNow,
            null,
            new SigningCredentials(
                AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}