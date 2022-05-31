using Application.Common.Interfaces;
using OOP_WebApp.Domain.Entities;

namespace Application.Tests;

public class TestService : ITestService
{
    private readonly IUnitOfWorkFactory _uowFactory;

    public TestService(IUnitOfWorkFactory uowFactory) => _uowFactory = uowFactory;

    public async Task<GetTestResponse> Get(TestId id, CancellationToken cancellationToken)
    {
        await using var uow = _uowFactory.Create();

        var test = await uow.Tests.Get(id, cancellationToken);

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

    public async Task<TestId> Create(CreateTestRequest request, UserId userId, CancellationToken cancellationToken)
    {
        await using var uow = _uowFactory.Create();

        var testId = TestId.From(Guid.NewGuid());
        await uow.Tests.Create(new Test(testId, request.Title, request.Description, userId), cancellationToken);

        var questions = new List<Question>();
        foreach (var questionRequest in request.Questions)
        {
            var questionId = QuestionId.From(Guid.NewGuid());
            questions.Add(new Question(questionId, questionRequest.String, testId));

            var options = questionRequest.Options
                .Select(o => new Option(OptionId.From(Guid.NewGuid()), o.String, questionId));

            await uow.Options.Create(options, cancellationToken);
        }

        await uow.Questions.Create(questions, cancellationToken);

        await uow.SaveChangesAsync();

        return testId;
    }

    public async Task Delete(TestId id, CancellationToken cancellationToken)
    {
        await using var uow = _uowFactory.Create();

        await uow.Tests.Delete(id, cancellationToken);

        await uow.SaveChangesAsync();
    }
}
