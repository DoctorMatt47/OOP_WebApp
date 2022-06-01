using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Application.Users;

public record AuthenticateRequest(Username Username, string Password);