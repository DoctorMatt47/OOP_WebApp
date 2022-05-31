using System.Data.Common;
using Application.Common.Interfaces;
using Npgsql;
using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.ValueObjects;

namespace Infrastructure.Repositories;

public class OptionRepository : RepositoryBase, IOptionRepository
{
    public OptionRepository(DbConnection connection) : base(connection)
    {
    }

    public async Task<IEnumerable<Option>> Get(QuestionId id, CancellationToken cancellationToken)
    {
        const string sql = @"SELECT * FROM ""Option"" WHERE ""QuestionId"" = @questionId";
        var parameter = new NpgsqlParameter("@questionId", id.Value);

        await using var command = await CreateSqlCommandAsync(sql, parameter, cancellationToken);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        var options = new List<Option>();
        while (await reader.ReadAsync(cancellationToken))
        {
            var idString = reader.GetString(0);
            var optionString = reader.GetString(1);
            var questionId = reader.GetString(2);

            options.Add(new Option(
                OptionId.From(Guid.Parse(idString)),
                OptionString.From(optionString),
                QuestionId.From(Guid.Parse(questionId))));
        }

        return options;
    }

    public async Task Create(IEnumerable<Option> options, CancellationToken cancellationToken)
    {
        var values = options
            .Select(o => @$"('{o.Id.Value}', '{o.String.Value}', '{o.QuestionId.Value}')")
            .Aggregate("", (acc, o) => $"{o}, {acc}")
            .SkipLast(2)
            .Aggregate("", (acc, c) => acc + c);

        const string sql = @"INSERT INTO ""Option"" (""Id"", ""String"", ""QuestionId"") VALUES @values";
        var parameter = new NpgsqlParameter("@values", values);

        await using var command = await CreateSqlCommandAsync(sql, parameter, cancellationToken);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
