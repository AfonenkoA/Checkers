using System.Data;
using System.Data.SqlClient;

namespace WebService.Repository.MSSqlImplementation;

public class RepositoryBase
{
    public const string Id = "id";
    public const string IdVar = "@id";
    public const string StringType = "NVARCHAR(MAX)";
    public const string UniqueStringType = "NVARCHAR(300)";
    public const string BinaryType = "VARBINARY(MAX)";
    public const string Schema = "Checkers.dbo";
    public const string Identity = $"{Id}			INT				NOT NULL	IDENTITY(1,1)	PRIMARY KEY";
    public const int InvalidId = -1;
    internal const string ReturnValue = "@RETURN_VALUE";

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

    protected RepositoryBase(SqlConnection connection)
    {
        _connection = connection;
    }

    protected SqlCommand CreateProcedure(string name)=> 
        new(name, Connection){CommandType = CommandType.StoredProcedure};

    protected SqlCommand CreateProcedureReturn(string name)
    {
        var cmd = CreateProcedure(name);
        cmd.Parameters.Add(new SqlParameter(ReturnValue, SqlDbType.Int));
        cmd.Parameters[ReturnValue].Direction = ParameterDirection.ReturnValue;
        return cmd;
    }

    public static string Fk(string t1, string t2, string s ="") =>
        $"CONSTRAINT FK_{Unwrap(t1)}_{Unwrap(t2)}{s}    FOREIGN KEY REFERENCES {t2}({Id})";
    internal static string Unwrap(string s) => s.Replace("[", "").Replace("]", "");

}