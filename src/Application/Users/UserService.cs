using OOP_WebApp.Application.Common.Exceptions;
using OOP_WebApp.Application.Common.Interfaces;
using OOP_WebApp.Application.Identity;
using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Application.Users;

public class UserService : IUserService
{
    private readonly IUnitOfWorkFactory _factory;
    private readonly IHashService _hash;
    private readonly IJwtTokenService _jwtToken;

    public UserService(IUnitOfWorkFactory factory, IJwtTokenService jwtToken, IHashService hash)
    {
        _factory = factory;
        _jwtToken = jwtToken;
        _hash = hash;
    }

    public async Task<AuthenticateResponse> Authenticate(
        AuthenticateRequest request,
        CancellationToken cancellationToken)
    {
        await using var uow = _factory.Create();

        var user = await uow.Users.Get(request.Username, cancellationToken);
        if (user is null) throw new BadRequestException("Incorrect username or password");

        var passwordHash = _hash.HashPassword(request.Password, Convert.FromBase64String(user.PasswordSalt));
        if (user.PasswordHash != passwordHash) throw new BadRequestException("Incorrect username or password");

        var token = _jwtToken.Get(user.Id, user.Role);
        return new AuthenticateResponse(token);
    }

    public async Task<AuthenticateResponse> Create(CreateUserRequest request, CancellationToken cancellationToken)
    {
        await using var uow = _factory.Create();

        var user = await uow.Users.Get(request.Username, cancellationToken);
        if (user is not null) throw new ConflictException($"There is user with username = {request.Username.Value}");

        var salt = _hash.GenerateSalt();
        var passwordHash = _hash.HashPassword(request.Password, salt);

        user = new User(request.Username, request.Role, passwordHash, Convert.ToBase64String(salt));
        await uow.Users.Create(user, cancellationToken);
        await uow.SaveChangesAsync();

        var token = _jwtToken.Get(user.Id, user.Role);
        return new AuthenticateResponse(token);
    }

    public async Task<IEnumerable<GetUserResponse>> Get(TestId testId, CancellationToken cancellationToken)
    {
        await using var uow = _factory.Create();
        var users = await uow.Users.Get(testId, cancellationToken);
        return users.Select(u => new GetUserResponse(u.Id, u.Role));
    }
}