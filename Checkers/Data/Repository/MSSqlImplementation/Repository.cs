using System.Data;
using Microsoft.Data.SqlClient;

namespace Checkers.Data.Repository.MSSqlImplementation;

public class Repository
{
    public const string Id = "id";
    public const string IdVar = "@id";
    public const string StringType = "NVARCHAR(MAX)";
    public const string UniqueStringType = "NVARCHAR(300)";
    public const string BinaryType = "VARBINARY(MAX)";
    public const string Schema = "Checkers.dbo";
    public const string Identity = $"{Id}			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY";
    public const int InvalidId = -1;


    private readonly SqlConnection _connection;

    private SqlConnection Connection
    {
        get
        {
            lock (this)
            {
                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();
                return _connection;
            }
        }
    }

    protected Repository(SqlConnection connection)
    {
        _connection = connection;
    }

    protected SqlCommand CreateProcedure(string name)=> new(name, Connection){CommandType = CommandType.StoredProcedure};
    
    public static string Fk(string t1, string t2, string s ="") =>
        $"CONSTRAINT FK_{Unwrap(t1)}_{Unwrap(t2)}{s}    FOREIGN KEY REFERENCES {t2}({Id})";
    internal static string Unwrap(string s) => s.Replace("[", "").Replace("]", "");

}