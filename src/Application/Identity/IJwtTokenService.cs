using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.EntityEnums;

namespace Application.Identity;

public interface IJwtTokenService
{
    string Get(Username username, Role role);
}
