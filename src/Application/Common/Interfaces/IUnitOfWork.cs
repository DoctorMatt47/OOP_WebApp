namespace Application.Common.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IOptionRepository Options { get; }
    IQuestionRepository Questions { get; }
    ITestRepository Tests { get; }
    IUserRepository Users { get; }

    Task SaveChangesAsync();
}