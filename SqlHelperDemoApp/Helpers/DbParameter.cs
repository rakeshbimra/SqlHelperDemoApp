namespace SqlHelperDemoApp.Helpers;

/// <summary>
/// Represents a database parameter with a name, value, and type information.
/// </summary>
public class DbParameter : IDbParameter
{
    /// <summary>
    /// Gets or sets the name of the database parameter.
    /// </summary>
    public string? ParameterName { get; set; }

    /// <summary>
    /// Gets or sets the type name of the database parameter.
    /// </summary>
    public string? TypeName { get; set; }

    /// <summary>
    /// Gets or sets the value of the database parameter.
    /// </summary>
    public object? Value { get; set; }

    /// <summary>
    /// Creates a new instance of <see cref="DbParameter"/> with the specified name, value, and optional type name.
    /// </summary>
    /// <param name="parameterName">The name of the parameter.</param>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="typeName">The type name of the parameter (optional).</param>
    /// <returns>A new <see cref="DbParameter"/> instance.</returns>
    public static DbParameter Create(string parameterName, object value, string typeName = "")
    {
        return new DbParameter()
        {
            ParameterName = parameterName,
            Value = value,
            TypeName = typeName
        };
    }
}