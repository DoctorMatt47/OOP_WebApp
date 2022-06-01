using OOP_WebApp.Application.Common.Exceptions;
using OOP_WebApp.Application.Common.Interfaces;
using OOP_WebApp.Application.Common.Responses;
using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Application.Tests;

public class TestService : ITestService
{
    private readonly IUnitOfWorkFactory _uowFactory;

    public TestService(IUnitOfWorkFactory uowFactory) => _uowFactory = uowFactory;

    public async Task<GetTestResponse> Get(TestId id, CancellationToken cancellationToken)
    {
        await using var uow = _uowFactory.Create();

        var test = await uow.Tests.Get(id, cancellationToken);
        if (test is null) throw new NotFoundException($"There is no test with id = {id.Value}");

        var questions = await uow.Questions.Get(test.Id, cancellationToken);

        var questionResponses = new List<GetQuestionResponse>();
        foreach (var question in questions)
        {
            var options = await uow.Options.Get(question.Id, cancellationToken);

            var optionResponses = options.Select(o => new GetOptionResponse(o.Id, o.String));
            questionResponses.Add(new GetQuestionResponse(question.Id, question.String, optionResponses));
        }

        return new GetTestResponse(test.Id, test.Title, test.Description, test.UserId, questionResponses);
    }

    public async Task<GuidIdResponse> Create(
        CreateTestRequest request, Username userId, CancellationToken cancellationToken)
    {
        await using var uow = _uowFactory.Create();

        var testId = TestId.From(Guid.NewGuid());

        var questions = new List<Question>();
        var optionLists = new List<IEnumerable<Option>>();
        foreach (var questionRequest in request.Questions)
        {
            var questionId = QuestionId.From(Guid.NewGuid());

            optionLists.Add(questionRequest.Options
                .Select(o => new Option(OptionId.From(Guid.NewGuid()), o.String, questionId)));

            questions.Add(new Question(questionId, questionRequest.String, testId));
        }

        var test = new Test(testId, request.Title, request.Description, userId);
        await uow.Tests.Create(test, cancellationToken);
        await uow.Questions.Create(questions, cancellationToken);
        foreach (var options in optionLists) await uow.Options.Create(options, cancellationToken);

        await uow.SaveChangesAsync();

        return new GuidIdResponse(testId.Value);
    }

    public async Task Delete(TestId id, CancellationToken cancellationToken)
    {
        await using var uow = _uowFactory.Create();

        await uow.Tests.Delete(id, cancellationToken);

        await uow.SaveChangesAsync();
    }
}