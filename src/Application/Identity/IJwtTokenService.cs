using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.EntityEnums;

namespace OOP_WebApp.Application.Identity;

public interface IJwtTokenService
{
    string Get(Username username, Role role);
}