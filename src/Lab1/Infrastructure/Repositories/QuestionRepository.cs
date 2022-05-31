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
            var idString = reader.GetString(0);
            var questionString = reader.GetString(1);
            var testId = reader.GetString(2);

            options.Add(new Question(
                QuestionId.From(Guid.Parse(idString)),
                QuestionString.From(questionString),
                TestId.From(Guid.Parse(testId))));
        }

        return options;
    }

    public async Task Create(IEnumerable<Question> options, CancellationToken cancellationToken)
    {
        var values = options
            .Select(o => @$"('{o.Id.Value}', '{o.String.Value}', '{o.TestId.Value}')")
            .Aggregate("", (acc, o) => $"{o}, {acc}")
            .SkipLast(2)
            .Aggregate("", (acc, c) => acc + c);

        const string sql = @"INSERT INTO ""Question"" (""Id"", ""String"", ""TestId"") VALUES @values";
        var parameter = new NpgsqlParameter("@values", values);

        await using var command = await CreateSqlCommandAsync(sql, parameter, cancellationToken);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
