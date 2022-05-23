using Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.ValueObjects;

namespace Infrastructure.Repositories;

public class OptionRepository : IOptionRepository
{
    private readonly string _connectionString;

    public OptionRepository(IConfiguration configuration) =>
        _connectionString = configuration
            .GetSection("ConnectionStrings")
            .GetSection("OOP_WebApp").Value;

    public async Task<IEnumerable<Option>> Get(QuestionId id, CancellationToken cancellationToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"SELECT * FROM ""Option"" WHERE ""QuestionId"" = @questionId";
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.Add(new NpgsqlParameter("@questionId", id.Value));

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        var options = new List<Option>();
        while (await reader.ReadAsync(cancellationToken))
        {
            var optionString = reader.GetString(0);
            var questionId = reader.GetString(1);

            options.Add(new Option(OptionString.From(optionString), new QuestionId(Guid.Parse(questionId))));
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

        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        const string sql = @"INSERT INTO ""Option"" (""Id"", ""String"", ""QuestionId"") VALUES @values";
        await using var command = new NpgsqlCommand(sql, connection);
        command.Parameters.Add(new NpgsqlParameter("@values", values));

        var query = command.Parameters.ToArray().Aggregate(command.CommandText,
            (current, p) => current.Replace(p.ParameterName, p.Value?.ToString()));

        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}
