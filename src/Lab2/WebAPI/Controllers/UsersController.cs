using Application.Users;
using Microsoft.AspNetCore.Mvc;

namespace Lab2.WebAPI.Controllers;

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