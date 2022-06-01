using System.Data;
using System.Data.Common;
using Application.Common.Interfaces;
using Npgsql;
using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.ValueObjects;

namespace Infrastructure.Repositories;

public class QuestionRepository : RepositoryBase, IQuestionRepository
{
    public QuestionRepository(DbConnection connection) : base(connection)
    {
    }

    public async Task<IEnumerable<Question>> Get(TestId id, CancellationToken cancellationToken)
    {
        const string sql = @"SELECT * FROM ""Question"" WHERE ""TestId"" = @testId";
        var parameter = new NpgsqlParameter("@testId", id.Value);

        await using var command = await CreateSqlCommandAsync(sql, parameter, cancellationToken);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        var options = new List<Question>();
        while (await reader.ReadAsync(cancellationToken))
        {
            var idString = reader.GetGuid(0);
            var questionString = reader.GetString(1);
            var testId = reader.GetGuid(2);

            options.Add(new Question(
                QuestionId.From(idString),
                QuestionString.From(questionString),
                TestId.From(testId)));
        }

        return options;
    }

    public async Task Create(IEnumerable<Question> questions, CancellationToken cancellationToken)
    {
        const string sql = @"INSERT INTO ""Question"" (""Id"", ""String"", ""TestId"") VALUES (@id, @string, @testId)";
        var parameters = new NpgsqlParameter[]
        {
            new("@id", SqlDbType.NChar),
            new("@string", SqlDbType.NChar),
            new("@testId", SqlDbType.NChar)
        };

        await using var command = await CreateSqlCommandAsync(sql, parameters, cancellationToken);
        foreach (var question in questions)
        {
            parameters[0].Value = question.Id.Value;
            parameters[1].Value = question.String.Value;
            parameters[2].Value = question.TestId.Value;
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }
}