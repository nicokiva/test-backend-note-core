using System.Runtime.CompilerServices;
using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using Test.Domain.Extensions;

namespace Test.Application.Queries.Base;

public class QueryRunner
{
    private static object EmptyParameter = new {};
    private readonly string _connectionString;
    private readonly ILogger<QueryRunner> _logger;

    public QueryRunner(QueryConnectionString connectionString, ILogger<QueryRunner> logger)
    {
        _logger = logger;
        _connectionString = connectionString.Value;
    }

    protected async Task<IEnumerable<dynamic>> RunQueryAsync(string query, object? queryParameters = null, CancellationToken cancellationToken = default(CancellationToken), [CallerMemberName] string queryReference = "")
    {
        try
        {
            queryReference = queryReference ?? this.GetGenericTypeName();
            if (queryParameters == null)
                queryParameters = EmptyParameter;

            _logger.LogInformation("----- Running Query - Name: {@Name}", queryReference);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                return await connection.QueryAsync(query, queryParameters);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "----- Run Query {@Name} exception", queryReference);
            throw;
        }
        finally
        {
            _logger.LogInformation("----- End Running Query - Name: {@Name}", queryReference);
        }
    }
    
    protected async Task<dynamic> RunQueryFirstAsync(string query, object queryParameters, CancellationToken cancellationToken, [CallerMemberName] string queryReference = "")
    {
        try
        {
            queryReference = queryReference ?? nameof(this.GetType);

            _logger.LogInformation("----- Running Query - Name: {@Name}", queryReference);

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                return await connection.QueryFirstOrDefaultAsync(query,queryParameters);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "----- Run Query {@Name} exception", queryReference);
            throw;
        }
        finally
        {
            _logger.LogInformation("----- End Running Query - Name: {@Name}", queryReference);
        }
    }
}