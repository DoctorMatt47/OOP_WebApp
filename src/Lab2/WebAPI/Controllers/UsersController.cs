using Microsoft.AspNetCore.Mvc;
using OOP_WebApp.Application.Users;
using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Lab2.WebAPI.Controllers;

public class UsersController : ApiControllerBase
{
    private readonly IUserService _users;

    public UsersController(IUserService users) => _users = users;

    [HttpGet]
    public Task<IEnumerable<GetUserResponse>> Get([FromQuery] TestId testId, CancellationToken cancellationToken) =>
        _users.Get(testId, cancellationToken);

    [HttpPost("Authenticate")]
    public Task<AuthenticateResponse> Authenticate(AuthenticateRequest request, CancellationToken cancellationToken) =>
        _users.Authenticate(request, cancellationToken);

    [HttpPost]
    public Task<AuthenticateResponse> Create(CreateUserRequest request, CancellationToken cancellationToken) =>
        _users.Create(request, cancellationToken);
}