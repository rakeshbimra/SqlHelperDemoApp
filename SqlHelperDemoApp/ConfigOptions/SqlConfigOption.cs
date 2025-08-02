namespace SqlHelperDemoApp.ConfigOptions;

public class SqlConfigOption
{
    public string? ConnectionString { get; set; }
    public int CommandTimeout { get; set; } = 30; // Default command timeout in seconds
}
