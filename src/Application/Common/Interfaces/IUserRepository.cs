using OOP_WebApp.Domain.Entities;

namespace Application.Common.Interfaces;

public interface IUserRepository
{
    Task<User?> Get(Username id, CancellationToken cancellationToken);
    Task Create(User user, CancellationToken cancellationToken);
}