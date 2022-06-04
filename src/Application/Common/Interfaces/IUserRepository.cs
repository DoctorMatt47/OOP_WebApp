using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Application.Common.Interfaces;

public interface IUserRepository
{
    Task<User?> Get(Username id, CancellationToken cancellationToken);
    Task<IEnumerable<User>> Get(TestId id, CancellationToken cancellationToken);
    Task Create(User user, CancellationToken cancellationToken);
}