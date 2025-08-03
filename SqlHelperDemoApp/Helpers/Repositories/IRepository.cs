using SqlHelperDemoApp.Helpers.TableTypes;


namespace SqlHelperDemoApp.Helpers.Repositories;

public interface IRepository
{
    /// <summary>
    /// Updates user information in the database using the provided collection of <see cref="UserTableType"/> objects.
    /// </summary>
    /// <param name="tableTypes">A collection of user data to be updated.</param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains <c>true</c> if the update was successful; otherwise, <c>false</c>.
    /// </returns>
    Task<bool> UpdateUser(IEnumerable<UserTableType> tableTypes);
}
