using System.Data;
using System.Data.Common;
using Npgsql;
using OOP_WebApp.Application.Common.Interfaces;
using OOP_WebApp.Domain.Entities;

namespace OOP_WebApp.Lab1.Infrastructure.Repositories;

public class AnswerRepository : RepositoryBase, IAnswerRepository
{
    public AnswerRepository(DbConnection connection) : base(connection)
    {
    }

    public async Task<IEnumerable<Answer>> Get(TestId id, Username username, CancellationToken cancellationToken)
    {
        const string sql = @"SELECT * FROM ""Answer"" WHERE ""TestId"" = @testId";
        var parameter = new NpgsqlParameter("@testId", id.Value);

        await using var command = await CreateSqlCommandAsync(sql, parameter, cancellationToken);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        var answers = new List<Answer>();
        while (await reader.ReadAsync(cancellationToken)) answers.Add(GetAnswerFromReader(reader));

        return answers;
    }

    public async Task Create(IEnumerable<Answer> answers, CancellationToken cancellationToken) =>
        throw new NotImplementedException();

    private static Answer GetAnswerFromReader(IDataRecord reader)
    {
        var answerId = reader.GetGuid(0);
        var username = reader.GetString(1);
        var testId = reader.GetGuid(2);
        var questionId = reader.GetGuid(3);
        var optionId = reader.GetGuid(4);

        return new Answer(
            AnswerId.From(answerId),
            Username.From(username),
            TestId.From(testId),
            QuestionId.From(questionId),
            OptionId.From(optionId));
    }
}
