namespace Application.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IOptionRepository Options { get; }
    IQuestionRepository Questions { get; }
    ITestRepository Tests { get; }
}
