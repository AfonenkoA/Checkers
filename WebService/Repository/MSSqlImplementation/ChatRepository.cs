using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Common.Entity;
using WebService.Repository.Interface;
using static System.Data.SqlDbType;
using static WebService.Repository.MSSqlImplementation.UserRepository;
using static WebService.Repository.MSSqlImplementation.MessageRepository;
using static WebService.Repository.MSSqlImplementation.SqlExtensions;

namespace WebService.Repository.MSSqlImplementation;

public sealed class ChatRepository : RepositoryBase, IChatRepository
{
    public const string ChatTable = "[Chat]";
    public const string ChatTypeTable = "[ChatType]";

    public const string ChatTypeName = "[chat_type_name]";
    public const string ChatTypeId = "[chat_type_id]";
    public const string ChatId = "[chat_id]";
    public const string ChatName = "[chat_name]";

    public const string CreateChatProc = "[SP_CreateChat]";
    public const string GetChatTypeByNameProc = "[SP_GetChatTypeByName]";
    public const string GetCommonChatIdProc = "[GetCommonChatId]";

    public const string ChatNameVar = "@chat_name";
    public const string ChatIdVar = "@chat_id";
    public const string ChatTypeNameVar = "@chat_type_name";

    public const string CommonChatName = "Common chat";

    internal ChatRepository(SqlConnection connection) : base(connection){}

    public IEnumerable<Message> GetMessages(Credential credential, int chatId, DateTime from)
    {
        using var command = CreateProcedure(SelectMessageProc);
        command.Parameters.AddRange(new[]
        {
            new SqlParameter { ParameterName = ChatIdVar, SqlDbType = Int, Value = chatId },
            new SqlParameter {ParameterName = LoginVar,SqlDbType = NVarChar,Value = credential.Login},
            new SqlParameter {ParameterName = PasswordVar,SqlDbType = NVarChar,Value = credential.Password}
        });
        using var reader = command.ExecuteReader();
        return reader.GetAllMessage();
    }

    public bool CreateMessage(Credential credential, int chatId, string message)
    {
        using var command = CreateProcedure(SendMessageProc);
        command.Parameters.AddRange(new[]
        {
            LoginParameter(credential.Login),
            PasswordParameter(credential.Password),
            new SqlParameter { ParameterName = ChatIdVar, SqlDbType = Int, Value = chatId },
            new SqlParameter {ParameterName = MessageContentVar,SqlDbType = NVarChar,Value = message}
        });
        return command.ExecuteNonQuery() > 0;
    }

    public int GetCommonChatId(Credential credential)
    {
        using var command = CreateProcedureReturn(GetCommonChatIdProc);
        command.ExecuteScalar();
        return command.GetReturn();
    }

}