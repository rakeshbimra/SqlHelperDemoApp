
namespace SqlHelperDemoApp.Helpers;

public static class Constant
{
    public static class DbParameter
    {
        public static readonly string Id = "@Id";
        public static readonly string FirstName = "@FirstName";
        public static readonly string LastName = "@LastName";
        public static readonly string Email = "@Email";
        public static readonly string UserData = "@UserData";
    }

    public static class NumericType
    {
        public static readonly int Zero = 0;
        public static readonly int One = 1;
        public static readonly int Two = 2;
        public static readonly int Three = 3;
        public static readonly int Four = 4;
    }

    public static class  UserDefinedTableType
    {
        public static readonly string UserTableType = "[dbo].[USER_TABLE_TYPE]";
    }

    public static class StoredProcedure
    {
        public static readonly string UpdateUser = "UPDATE_USER";
    }
}
