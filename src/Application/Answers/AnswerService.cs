using OOP_WebApp.Application.Common.Interfaces;
using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Application.Answers;

public class AnswerService : IAnswerService
{
    private readonly IUnitOfWorkFactory _uowFactory;

    public AnswerService(IUnitOfWorkFactory uowFactory) => _uowFactory = uowFactory;

    public async Task<IEnumerable<GetAnswerResponse>> Get(
        TestId id,
        Username username,
        CancellationToken cancellationToken)
    {
        await using var uow = _uowFactory.Create();
        var answers = await uow.Answers.Get(id, username, cancellationToken);
        return answers.Select(a => new GetAnswerResponse(a.Id, a.TestId, a.QuestionId, a.OptionId, a.Username));
    }

    public async Task Create(
        IEnumerable<CreateAnswerRequest> request,
        CancellationToken cancellationToken)
    {
        await using var uow = _uowFactory.Create();
        var answers = request.Select(a => new Answer(
            AnswerId.From(Guid.NewGuid()),
            a.Username,
            a.TestId,
            a.QuestionId,
            a.OptionId
        ));

        await uow.Answers.Create(answers, cancellationToken);
        await uow.SaveChangesAsync();
    }
}
