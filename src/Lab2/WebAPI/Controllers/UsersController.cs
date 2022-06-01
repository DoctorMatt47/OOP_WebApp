using Microsoft.AspNetCore.Mvc;
using OOP_WebApp.Application.Users;

namespace OOP_WebApp.Lab2.WebAPI.Controllers;

public class UsersController : ApiControllerBase
{
    private readonly IUserService _users;

    public UsersController(IUserService users) => _users = users;

    [HttpPost("Authenticate")]
    public Task<AuthenticateResponse> Authenticate(AuthenticateRequest request, CancellationToken cancellationToken) =>
        _users.Authenticate(request, cancellationToken);

    [HttpPost]
    public Task<AuthenticateResponse> Create(CreateUserRequest request, CancellationToken cancellationToken) =>
        _users.Create(request, cancellationToken);
}