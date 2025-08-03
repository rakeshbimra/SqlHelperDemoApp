using Microsoft.Data.SqlClient.Server;
using Microsoft.Extensions.Logging;
using SqlHelperDemoApp.Helpers.TableTypes;

namespace SqlHelperDemoApp.Helpers.Repositories;

/// <summary>
/// Provides repository methods for performing user-related data operations, such as updating user information in the database.
/// </summary>
public class Repository : IRepository
{
    /// <summary>
    /// The logger instance for logging repository operations.
    /// </summary>
    private readonly ILogger<Repository> logger;

    /// <summary>
    /// The database manager used to execute database commands.
    /// </summary>
    private readonly IDatabaseManager databaseManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository"/> class with the specified logger and database manager.
    /// </summary>
    /// <param name="logger">The logger instance for logging repository operations.</param>
    /// <param name="databaseManager">The database manager used to execute database commands.</param>
    public Repository(ILogger<Repository> logger, IDatabaseManager databaseManager)
    {
        this.logger = logger;
        this.databaseManager = databaseManager;
    }

    /// <summary>
    /// Updates user information in the database using the provided collection of <see cref="UserTableType"/> objects.
    /// </summary>
    /// <param name="tableTypes">A collection of user data to be updated.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains <c>true</c> if the update was successful; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> UpdateUser(IEnumerable<UserTableType> tableTypes)
    {
        List<IDbParameter> parameters = SetDbParameters(tableTypes);

        var @int = await this.databaseManager
            .ExecuteNonQueryAsync(Constant.StoredProcedure.UpdateUser, parameters);

        return @int.HasValue;
    }

    /// <summary>
    /// Creates a list of database parameters for the user update operation.
    /// </summary>
    /// <param name="tableTypes">A collection of user data to be converted into database parameters.</param>
    /// <returns>A list of <see cref="IDbParameter"/> objects representing the user data.</returns>
    private static List<IDbParameter> SetDbParameters(IEnumerable<UserTableType> tableTypes)
    {
        return new List<IDbParameter> {
            DbParameter.Create(Constant.DbParameter.UserData,
            GetUserDataRecords(tableTypes),
            Constant.UserDefinedTableType.UserTableType)
        };
    }

    /// <summary>
    /// Converts a collection of <see cref="UserTableType"/> objects into a collection of <see cref="SqlDataRecord"/> objects for use as table-valued parameters.
    /// </summary>
    /// <param name="tableTypes">A collection of user data to be converted.</param>
    /// <returns>A collection of <see cref="SqlDataRecord"/> objects representing the user data.</returns>
    private static IEnumerable<SqlDataRecord> GetUserDataRecords(IEnumerable<UserTableType> tableTypes)
    {
        ICollection<SqlDataRecord> sqlDataRecords = new List<SqlDataRecord>();
        SqlMetaData[] sqlMetaData = SetSqlMetaData();
        SetSqlDataRecords(tableTypes, sqlDataRecords, sqlMetaData);
        return sqlDataRecords;
    }

    /// <summary>
    /// Populates a collection of <see cref="SqlDataRecord"/> objects with user data from the provided <see cref="UserTableType"/> collection.
    /// </summary>
    /// <param name="tableTypes">A collection of user data to be added to the records.</param>
    /// <param name="dataRecords">The collection to populate with <see cref="SqlDataRecord"/> objects.</param>
    /// <param name="sqlMetaDatas">The metadata describing the structure of the SQL table type.</param>
    private static void SetSqlDataRecords(IEnumerable<UserTableType> tableTypes,
        ICollection<SqlDataRecord> dataRecords,
        SqlMetaData[] sqlMetaDatas)
    {
        SqlDataRecord row;
        foreach (var item in tableTypes)
        {
            row = new(sqlMetaDatas);
            row.SetValues(
                item.Id,
                item.FirstName,
                item.LastName,
                item.Email
                );
            dataRecords.Add(row);
        }
    }

    /// <summary>
    /// Defines the SQL metadata for the user table type used in table-valued parameters.
    /// </summary>
    /// <returns>An array of <see cref="SqlMetaData"/> objects describing the user table structure.</returns>
    private static SqlMetaData[] SetSqlMetaData()
    {
        SqlMetaData[] sqlMetaDatas = new SqlMetaData[Constant.NumericType.Four];
        sqlMetaDatas[Constant.NumericType.Zero] = new SqlMetaData(Constant.DbParameter.Id, System.Data.SqlDbType.UniqueIdentifier);
        sqlMetaDatas[Constant.NumericType.One] = new SqlMetaData(Constant.DbParameter.FirstName, System.Data.SqlDbType.NVarChar);
        sqlMetaDatas[Constant.NumericType.Two] = new SqlMetaData(Constant.DbParameter.LastName, System.Data.SqlDbType.NVarChar);
        sqlMetaDatas[Constant.NumericType.Three] = new SqlMetaData(Constant.DbParameter.Email, System.Data.SqlDbType.VarChar);
        return sqlMetaDatas;
    }
}