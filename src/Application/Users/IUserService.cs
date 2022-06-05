using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Application.Users;

public interface IUserService
{
    Task<IEnumerable<GetUserResponse>> Get(TestId testId, CancellationToken cancellationToken);
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest request, CancellationToken cancellationToken);
    Task<AuthenticateResponse> Create(CreateUserRequest request, CancellationToken cancellationToken);
}
