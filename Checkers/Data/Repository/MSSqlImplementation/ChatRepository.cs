using System;
using System.Collections.Generic;
using System.Data;
using Checkers.Data.Entity;
using Checkers.Data.Repository.Interface;
using Microsoft.Data.SqlClient;
using static Checkers.Data.Repository.MSSqlImplementation.UserRepository;

namespace Checkers.Data.Repository.MSSqlImplementation;

public sealed class ChatRepository : MessageRepository, IChatRepository
{
    public const string ChatTable = "[Chat]";
    public const string ChatTypeTable = "[ChatType]";

    public const string ChatTypeName = "[chat_type_name]";
    public const string ChatTypeId = "[chat_type_id]";
    public const string ChatId = "[chat_id]";
    public const string ChatName = "[chat_name]";

    public const string CreateChatProc = "[SP_CreateChat]";
    public const string ChatNameVar = "@chat_name";
    public const string ChatIdVar = "@chat_id";
    public const string ChatTypeNameVar = "@chat_type_name";
    public const string ChatTypeIdVar = "@chat_type_id";
    public const string CommonChatId = "1";

    public const string CommonChatName = "Common chat";

    public IEnumerable<Message> GetMessages(Credential credential, int chatId, DateTime from)
    {
        using var command = CreateProcedure(SelectMessageProc);
        command.Parameters.AddRange(new[]
        {
            new SqlParameter { ParameterName = ChatIdVar, SqlDbType = SqlDbType.Int, Value = chatId },
            new SqlParameter {ParameterName = LoginVar,SqlDbType = SqlDbType.NVarChar,Value = credential.Login},
            new SqlParameter {ParameterName = PasswordVar,SqlDbType = SqlDbType.NVarChar,Value = credential.Password}
        });
        using var reader = command.ExecuteReader();
        List<Message> list = new();
        while (reader.Read())
            list.Add(new Message
            {
                Id = reader.GetFieldValue<int>(Id),
                SendTime = reader.GetFieldValue<DateTime>(SendTime),
                UserId = reader.GetFieldValue<int>(UserId),
                Content = reader.GetFieldValue<string>(MessageContent)
            });
        return list;
    }

    public bool CreateMessage(Credential credential, int chatId, string message)
    {
        using var command = CreateProcedure(SendMessageProc);
        command.Parameters.AddRange(new[]
        {
            new SqlParameter { ParameterName = ChatIdVar, SqlDbType = SqlDbType.Int, Value = chatId },
            new SqlParameter {ParameterName = LoginVar,SqlDbType = SqlDbType.NVarChar,Value = credential.Login},
            new SqlParameter {ParameterName = PasswordVar,SqlDbType = SqlDbType.NVarChar,Value = credential.Password},
            new SqlParameter {ParameterName = MessageContent,SqlDbType = SqlDbType.NVarChar,Value = message}
        });
        return command.ExecuteNonQuery() > 0;
    }

}