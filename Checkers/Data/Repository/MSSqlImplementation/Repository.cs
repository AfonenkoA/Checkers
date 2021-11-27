using System;
using System.Data;
using System.Reflection;
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

    public sealed class Factory
    {
        private static readonly Exception Fail = new InvalidOperationException();
        private readonly SqlConnection connection;
        public Factory(string connection)
        {
            this.connection = new SqlConnection(connection);
        }

        public T Get<T>() where T : Repository
        {
            var type = typeof(T);
            var instance = type.Assembly.CreateInstance(
                type.FullName ?? throw Fail, false,
                BindingFlags.Instance | BindingFlags.NonPublic,
                null, new object[]{connection}, null, null);
            return (T?) instance ?? throw Fail;
        }
    }

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