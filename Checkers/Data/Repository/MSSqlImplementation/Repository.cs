using System.Data;
using Microsoft.Data.SqlClient;

namespace Checkers.Data.Repository.MSSqlImplementation;

public class Repository
{
    public const string Id = "id";
    public const string IdVar = "@id";
    public const string StringType = "NVARCHAR(300)";
    public const string BinaryType = "VARBINARY(MAX)";
    public const string Schema = "Checkers.dbo";
    public const string Identity = $"{Id}				INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY";
    public const string InvalidId = "-1";

    private const string ConnectionString = @"Data Source=DELL;Initial Catalog=Checkers;Integrated Security=True";
    internal static SqlCommand CreateProcedure(string name)=> new(name, Connection){CommandType = CommandType.StoredProcedure};

    public static string Fk(string s1, string s2, string s ="") => $"CONSTRAINT FK_{Unwrap(s1)}_{Unwrap(s2)}{s}    FOREIGN KEY REFERENCES {s2}({Id})";

    internal static string Unwrap(string s) => s.Replace("[", "").Replace("]", "");


    private static readonly SqlConnection DatabaseConnection = new(ConnectionString);
    private static bool _isClosed = true;

    protected static SqlConnection Connection
    {
        get
        {
            if (!_isClosed) return DatabaseConnection;
            DatabaseConnection.Open();
            _isClosed = false;
            return DatabaseConnection;
        }
    }
}

internal static class SqlExtensions
{
    internal static int GetReturn(this SqlCommand command) => (int)command.Parameters["@RETURN_VALUE"].Value;
    internal static T GetFieldValue<T>(this SqlDataReader reader,string col) => (T) reader[Repository.Unwrap(col)];
}