using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Application.Common.Interfaces;

public interface IOptionRepository
{
    Task<IEnumerable<Option>> Get(QuestionId id, CancellationToken cancellationToken);
    Task Create(IEnumerable<Option> options, CancellationToken cancellationToken);
}