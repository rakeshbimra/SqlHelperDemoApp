namespace SqlHelperDemoApp.Helpers;

/// <summary>
/// Defines the contract for a database manager that provides methods for executing SQL commands and managing database operations.
/// </summary>
public interface IDatabaseManager
{
    /// <summary>
    /// Executes a stored procedure as a non-query asynchronously.
    /// </summary>
    /// <param name="storedProcedure">The name of the stored procedure to execute.</param>
    /// <param name="parameters">The collection of parameters to pass to the stored procedure (optional).</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete (optional).</param>
    /// <returns>The number of rows affected, or null if execution fails.</returns>
    /// <exception cref="SqlException">Thrown when a SQL Server error occurs during execution.</exception>
    Task<int?> ExecuteNonQueryAsync(string storedProcedure, IEnumerable<IDbParameter>? parameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes a stored procedure asynchronously and maps the result set to a list of objects of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type to which each row in the result set will be mapped. The type must have a parameterless constructor and settable properties matching the column names.</typeparam>
    /// <param name="storedProcedure">The name of the stored procedure to execute.</param>
    /// <param name="parameters">A dictionary of parameter names and values to pass to the stored procedure (optional).</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete (optional).</param>
    /// <returns>A list of objects of type <typeparamref name="T"/> representing the result set.</returns>
    Task<List<T>> ExecuteReaderAsync<T>(string storedProcedure, IDictionary<string, object>? parameters = null, CancellationToken cancellationToken = default);


}