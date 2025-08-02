using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SqlHelperDemoApp.ConfigOptions;
using System.Data;

namespace SqlHelperDemoApp.Helpers;

/// <summary>
/// Provides methods for executing SQL commands and managing database operations using SQL Server.
/// </summary>
public class DatabaseManager : IDatabaseManager
{
    private readonly ILogger<DatabaseManager> logger;
    private readonly SqlConfigOption sqlConfigOption;

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseManager"/> class with the specified logger and SQL configuration options.
    /// </summary>
    /// <param name="logger">The logger instance for logging database operations.</param>
    /// <param name="sqlConfigOption">The SQL configuration options.</param>
    public DatabaseManager(ILogger<DatabaseManager> logger, IOptions<SqlConfigOption> sqlConfigOption)
    {
        this.logger = logger;
        this.sqlConfigOption = sqlConfigOption.Value;
    }

    /// <summary>
    /// Executes a stored procedure as a non-query asynchronously.
    /// </summary>
    /// <param name="storedProcedure">The name of the stored procedure to execute.</param>
    /// <param name="parameters">The collection of parameters to pass to the stored procedure (optional).</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete (optional).</param>
    /// <returns>The number of rows affected, or null if execution fails.</returns>
    /// <exception cref="SqlException">Thrown when a SQL Server error occurs during execution.</exception>
    public async Task<int?> ExecuteNonQueryAsync(string storedProcedure, IEnumerable<IDbParameter>? parameters = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            logger.LogInformation("Executing SQL: {Sql}", storedProcedure);
            await using SqlConnection sqlConn = new(this.sqlConfigOption.ConnectionString);
            await using SqlCommand sqlCommand = new(storedProcedure, sqlConn);
            FillSqlParameters(parameters, sqlCommand);
            await sqlConn.OpenAsync(cancellationToken);
            logger.LogInformation("SQL executed successfully: {Sql}", storedProcedure);
            return await sqlCommand.ExecuteNonQueryAsync(cancellationToken);
        }
        catch (SqlException ex)
        {
            LogSqlError(storedProcedure, ex);
            throw;
        }
    }

    /// <summary>
    /// Adds the specified parameters to the given <see cref="SqlCommand"/>.
    /// </summary>
    /// <param name="parameters">The collection of parameters to add.</param>
    /// <param name="sqlCommand">The SQL command to which the parameters will be added.</param>
    private static void FillSqlParameters(IEnumerable<IDbParameter>? parameters, SqlCommand sqlCommand)
    {
        if (parameters != null)
        {
            foreach (var param in parameters)
            {
                if (!string.IsNullOrEmpty(param.TypeName))
                {
                    sqlCommand.Parameters.Add(new SqlParameter(param.ParameterName, SqlDbType.Structured)
                    {
                        TypeName = param.TypeName,
                        Value = param.Value
                    });
                    continue;
                }
                sqlCommand.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
            }
        }
    }

    /// <summary>
    /// Logs SQL errors using the configured logger.
    /// </summary>
    /// <param name="storedProcedure">The name of the stored procedure that caused the error.</param>
    /// <param name="ex">The SQL exception to log.</param>
    private void LogSqlError(string storedProcedure, SqlException ex)
    {
        string errorMessage = $"Error executing SQL: {storedProcedure}. Error Code: {ex.Number}, Message: {ex.Message}";
        this.logger.LogError(ex, errorMessage);
    }
}