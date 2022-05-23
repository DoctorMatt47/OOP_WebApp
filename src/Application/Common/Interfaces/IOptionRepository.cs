using OOP_WebApp.Domain.Entities;

namespace Application.Common.Interfaces;

public interface IOptionRepository
{
    Task<IEnumerable<Option>> Get(QuestionId id, CancellationToken cancellationToken);
    Task Create(IEnumerable<Option> options, CancellationToken cancellationToken);
}