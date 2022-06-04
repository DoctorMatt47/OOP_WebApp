using System.Data;
using System.Data.Common;
using Npgsql;
using OOP_WebApp.Application.Common.Interfaces;
using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.ValueObjects;

namespace OOP_WebApp.Lab1.Infrastructure.Repositories;

public class TestRepository : RepositoryBase, ITestRepository
{
    public TestRepository(DbConnection connection) : base(connection)
    {
    }

    public async Task<IEnumerable<Test>> Get(CancellationToken cancellationToken)
    {
        const string sql = @"SELECT * FROM ""Test""";

        await using var command = await CreateSqlCommandAsync(sql, new NpgsqlParameter[] { }, cancellationToken);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        var tests = new List<Test>();
        while (await reader.ReadAsync(cancellationToken)) tests.Add(GetTestFromReader(reader));

        return tests;
    }

    public async Task<Test?> Get(TestId id, CancellationToken cancellationToken)
    {
        const string sql = @"SELECT * FROM ""Test"" WHERE ""Id"" = @testId";
        var parameter = new NpgsqlParameter("@testId", id.Value);

        await using var command = await CreateSqlCommandAsync(sql, parameter, cancellationToken);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken)) return null;

        return GetTestFromReader(reader);
    }

    public async Task Create(Test test, CancellationToken cancellationToken)
    {
        const string sql =
            @"INSERT INTO ""Test"" (""Id"", ""Title"", ""Description"", ""Username"") VALUES (@id, @title, @description, @username)";
        var parameters = new NpgsqlParameter[]
        {
            new("@id", test.Id.Value),
            new("@title", test.Title.Value),
            new("@description", test.Description.Value),
            new("@username", test.Username.Value)
        };

        await using var command = await CreateSqlCommandAsync(sql, parameters, cancellationToken);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    public async Task Delete(TestId id, CancellationToken cancellationToken)
    {
        const string sql = @"DELETE FROM ""Test"" WHERE ""Id"" = @id";
        var parameter = new NpgsqlParameter("@id", id.Value);

        await using var command = await CreateSqlCommandAsync(sql, parameter, cancellationToken);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static Test GetTestFromReader(IDataRecord reader)
    {
        var idString = reader.GetGuid(0);
        var titleString = reader.GetString(1);
        var descriptionString = reader.GetString(2);
        var userId = reader.GetString(3);

        return new Test(
            TestId.From(idString),
            TestTitleString.From(titleString),
            TestDescriptionString.From(descriptionString),
            Username.From(userId));
    }
}
