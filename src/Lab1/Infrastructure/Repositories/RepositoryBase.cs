using System.Data.Common;
using Npgsql;

namespace Lab1.Infrastructure.Repositories;

public abstract class RepositoryBase
{
    protected readonly DbConnection Connection;

    protected RepositoryBase(DbConnection connection) => Connection = connection;

    protected Task<DbCommand> CreateSqlCommandAsync(
        string sql,
        NpgsqlParameter parameter,
        CancellationToken cancellationToken)
    {
        return CreateSqlCommandAsync(sql, new[] {parameter}, cancellationToken);
    }

    protected async Task<DbCommand> CreateSqlCommandAsync(
        string sql,
        IEnumerable<NpgsqlParameter> parameters,
        CancellationToken cancellationToken)
    {
        var command = Connection.CreateCommand();
        command.CommandText = sql;
        foreach (var parameter in parameters) command.Parameters.Add(parameter);

        return command;
    }
}