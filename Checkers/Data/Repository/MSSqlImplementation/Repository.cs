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
    public const string Identity = $"{Id}			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY";
    public const string InvalidId = "-1";

    private readonly SqlConnection _connection;

    private SqlConnection Connection
    {
        get
        {
            if (_isOpened) return _connection;
            _connection.Open();
            _isOpened = true;
            return _connection;
        }
    }
    private bool _isOpened;

    protected Repository(SqlConnection connection)
    {
        _connection = connection;
    }

    protected SqlCommand CreateProcedure(string name)=> new(name, Connection){CommandType = CommandType.StoredProcedure};
    
    public static string Fk(string s1, string s2, string s ="") =>
        $"CONSTRAINT FK_{Unwrap(s1)}_{Unwrap(s2)}{s}    FOREIGN KEY REFERENCES {s2}({Id})";
    internal static string Unwrap(string s) => s.Replace("[", "").Replace("]", "");

}

internal static class SqlExtensions
{
    internal static int GetReturn(this SqlCommand command) => (int)command.Parameters["@RETURN_VALUE"].Value;
    internal static T GetFieldValue<T>(this SqlDataReader reader, string col) => (T)reader[Repository.Unwrap(col)];
}