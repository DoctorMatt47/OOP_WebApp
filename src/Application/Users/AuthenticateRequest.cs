using OOP_WebApp.Domain.Entities;

namespace Application.Users;

public record AuthenticateRequest(Username Username, string Password);
