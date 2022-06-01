using System.Data;
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
            var idString = reader.GetGuid(0);
            var optionString = reader.GetString(1);
            var questionId = reader.GetGuid(2);

            options.Add(new Option(
                OptionId.From(idString),
                OptionString.From(optionString),
                QuestionId.From(questionId)));
        }

        return options;
    }

    public async Task Create(IEnumerable<Option> options, CancellationToken cancellationToken)
    {
        const string sql =
            @"INSERT INTO ""Option"" (""Id"", ""String"", ""QuestionId"") VALUES (@id, @string, @questionId);";
        var parameters = new NpgsqlParameter[]
        {
            new("@id", SqlDbType.NChar),
            new("@string", SqlDbType.NChar),
            new("@questionId", SqlDbType.NChar)
        };

        await using var command = await CreateSqlCommandAsync(sql, parameters, cancellationToken);
        foreach (var option in options)
        {
            parameters[0].Value = option.Id.Value;
            parameters[1].Value = option.String.Value;
            parameters[2].Value = option.QuestionId.Value;
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }
}
