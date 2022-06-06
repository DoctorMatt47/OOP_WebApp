using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OOP_WebApp.Application.Answers;
using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Lab2.WebAPI.Controllers;

public class AnswersController : ApiControllerBase
{
    private readonly IAnswerService _answers;

    public AnswersController(IAnswerService answers) => _answers = answers;

    [HttpGet]
    [Authorize(Roles = "Tutor")]
    public Task<IEnumerable<GetAnswerResponse>> Get(
        [FromQuery] Guid testId,
        [FromQuery] string username,
        CancellationToken cancellationToken) =>
        _answers.Get(TestId.From(testId), Username.From(username), cancellationToken);
    
    [HttpPost]
    [Authorize(Roles = "Student")]
    public Task Create(
        IEnumerable<CreateAnswerRequest> answers,
        CancellationToken cancellationToken) =>
        _answers.Create(answers, cancellationToken);
}
