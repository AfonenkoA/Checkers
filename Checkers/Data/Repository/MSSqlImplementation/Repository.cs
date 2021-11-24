using Microsoft.Data.SqlClient;

namespace Checkers.Data.Repository.MSSqlImplementation;

public class Repository
{
    public const string Id = "id";
    public const string IdVar = "@id";
    public const string StringType = "NVARCHAR(300)";
    public const string Schema = "Checkers.dbo";
    private const string ConnectionString = @"Data Source=DELL;Initial Catalog=Checkers;Integrated Security=True";

    public static string Fk(string s1, string s2) => $"FK_{Unwrap(s1)}_{Unwrap(s2)}";

    public static string Unwrap(string s) => s.Replace("[", "").Replace("]", "");


    private static readonly SqlConnection DatabaseConnection = new(ConnectionString);
    private static bool _isClosed = true;

    protected static SqlConnection Connection
    {
        get
        {
            if (_isClosed)
            {
                DatabaseConnection.Open();
                _isClosed = false;
            }
            return DatabaseConnection;
        }
    }
}

internal static class ReaderExtensions
{
    internal static T GetFieldValue<T>(this SqlDataReader reader,string col) => (T) reader[Repository.Unwrap(col)];
}