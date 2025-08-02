namespace SqlHelperDemoApp.Helpers;

/// <summary>
/// Defines the contract for a database parameter, including its name, type, and value.
/// </summary>
public interface IDbParameter
{
    /// <summary>
    /// Gets or sets the name of the database parameter.
    /// </summary>
    string? ParameterName { get; set; }

    /// <summary>
    /// Gets or sets the type name of the database parameter.
    /// </summary>
    string? TypeName { get; set; }

    /// <summary>
    /// Gets or sets the value of the database parameter.
    /// </summary>
    object? Value { get; set; }
}