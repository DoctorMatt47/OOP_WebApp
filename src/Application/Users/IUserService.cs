namespace OOP_WebApp.Application.Users;

public interface IUserService
{
    Task<AuthenticateResponse> Authenticate(
        AuthenticateRequest request,
        CancellationToken cancellationToken);

    Task<AuthenticateResponse> Create(CreateUserRequest request, CancellationToken cancellationToken);
}