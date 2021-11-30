using System;
using System.Data;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Microsoft.Data.SqlClient;
using static Checkers.Data.Repository.MSSqlImplementation.SqlExtensions;

namespace Checkers.Data.Repository.MSSqlImplementation;

public sealed class ResourceRepository : Repository, IResourceRepository
{
    public const string ResourceTable = "[ResourceTable]";

    public const string ResourceBytes = "[resource_bytes]";
    public const string ResourceId = "[resource_id]";
    public const string ResourceExtension = "[resource_extension]";
    

    public const string ResourceExtensionVar = "@resource_extension";
    public const string ResourceBytesVar = "@resource_bytes";
    public const string ResourceIdVar = "@resource_id";

    public const string CreateResourceProc= "[SP_CreateResource]";
    public const string SelectResourceProc = "[SP_SelectResource]";
    public const string CreateResourceFromFileProc = "[SP_CreateResourceFromFile]";

    internal ResourceRepository(SqlConnection connection) : base(connection) { }

    public int CreateFile(Credential credential, byte[] picture, string ext)
    {
        using var command = CreateProcedureReturn(CreateResourceProc);
        
            command.Parameters.AddRange(new []
            {
                new SqlParameter { ParameterName = ResourceExtensionVar, SqlDbType = SqlDbType.NVarChar, Value = ext },
                new SqlParameter { ParameterName = ResourceBytesVar, SqlDbType = SqlDbType.VarBinary,Value = picture}
            });
            command.ExecuteNonQuery();
            return command.GetReturn();
    }

    public (byte[], string) GetFile(int id)
    {
        using var command = CreateProcedure(SelectResourceProc);

        command.Parameters.Add(IdParameter(id));
        using var reader = command.ExecuteReader();
        return reader.Read() ? 
            (reader.GetFieldValue<byte[]>(ResourceBytes),reader.GetFieldValue<string>(ResourceExtension)) : 
            (Array.Empty<byte>(),string.Empty);
    }
}