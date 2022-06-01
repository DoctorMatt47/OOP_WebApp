using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.EntityEnums;

namespace OOP_WebApp.Application.Users;

public record CreateUserRequest(Username Username, Role Role, string Password);