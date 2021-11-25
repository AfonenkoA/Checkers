using System;
using System.Data;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Microsoft.Data.SqlClient;

namespace Checkers.Data.Repository.MSSqlImplementation;

public class ResourceRepository : Repository, IResourceRepository
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


    public int CreateFile(Credential credential, byte[] picture, string ext)
    {
        using var command = new SqlCommand(CreateResourceProc, Connection)
            {CommandType = CommandType.StoredProcedure};
        
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
        using var command = new SqlCommand(SelectResourceProc, Connection)
            { CommandType = CommandType.StoredProcedure };

        command.Parameters.Add(new SqlParameter { ParameterName = ResourceExtensionVar, SqlDbType = SqlDbType.Int, Value = id });

        using var reader = command.ExecuteReader();
        if(reader.Read())
            return (reader.GetFieldValue<byte[]>(ResourceBytes),reader.GetFieldValue<string>(ResourceExtension));
        return (Array.Empty<byte>(),string.Empty);
    }
}