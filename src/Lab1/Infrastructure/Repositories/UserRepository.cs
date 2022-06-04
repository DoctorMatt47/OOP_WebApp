using System.Data;
using System.Data.Common;
using Npgsql;
using OOP_WebApp.Application.Common.Interfaces;
using OOP_WebApp.Domain.Entities;
using OOP_WebApp.Domain.EntityEnums;

namespace OOP_WebApp.Lab1.Infrastructure.Repositories;

public class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(DbConnection connection) : base(connection)
    {
    }

    public async Task<User?> Get(Username id, CancellationToken cancellationToken)
    {
        const string sql = @"SELECT * FROM ""User"" WHERE ""Username"" = @username";
        var parameter = new NpgsqlParameter("@username", id.Value);

        await using var command = await CreateSqlCommandAsync(sql, parameter, cancellationToken);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken)) return null;

        var username = reader.GetString(0);
        var role = reader.GetInt32(1);
        var passwordHash = reader.GetString(2);
        var passwordSalt = reader.GetString(3);

        return new User(Username.From(username), (Role) role, passwordHash, passwordSalt);
    }

    public async Task<IEnumerable<User>> Get(TestId id, CancellationToken cancellationToken)
    {
        const string sql = @"SELECT * FROM ""User"" 
            JOIN (SELECT DISTINCT ""Username"" FROM ""Answer"" WHERE ""Username"" = @username) Answer
            on ""User"".""Username"" = Answer.""Username"" ";

        var parameter = new NpgsqlParameter("@username", id.Value);

        await using var command = await CreateSqlCommandAsync(sql, parameter, cancellationToken);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        var users = new List<User>();
        while (await reader.ReadAsync(cancellationToken)) users.Add(GetUserFromReader(reader));

        return users;
    }

    public async Task Create(User user, CancellationToken cancellationToken)
    {
        const string sql =
            @"INSERT INTO ""User"" (""Username"", ""Role"", ""PasswordHash"", ""PasswordSalt"") VALUES (@username, @role, @passwordHash, @passwordSalt)";
        var parameters = new NpgsqlParameter[]
        {
            new("@username", user.Id.Value),
            new("@role", (int) user.Role),
            new("@passwordHash", user.PasswordHash),
            new("@passwordSalt", user.PasswordSalt)
        };

        await using var command = await CreateSqlCommandAsync(sql, parameters, cancellationToken);
        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static User GetUserFromReader(IDataRecord reader)
    {
        var username = reader.GetString(0);
        var role = reader.GetInt32(1);
        var passwordHash = reader.GetString(2);
        var passwordSalt = reader.GetString(3);

        var user = new User(Username.From(username), (Role) role, passwordHash, passwordSalt);
        return user;
    }
}
