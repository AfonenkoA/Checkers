using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Interface;
using Api.WebImplementation;
using Common.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ApiTest;

[TestClass]
public class ChatTest
{
    private static readonly IAsyncChatApi ChatApi = new ChatWebApi();
    private static readonly Credential Credential = new() {Login = "roflan", Password = "Rofl123"};
    private const string Message = "Test message";

    [TestMethod]
    public async Task Test01SendToAllChat()
    {
        var (getChatSuccess, id) = await ChatApi.TryGetCommonChatId(Credential);
        IsTrue(getChatSuccess);
        var success = await ChatApi.SendMessage(Credential, id, Message);
        IsTrue(success);
    }

    [TestMethod]
    public async Task Test02GetFromAllChat()
    {
        var (getChatSuccess, id) = await ChatApi.TryGetCommonChatId(Credential);
        IsTrue(getChatSuccess);
        var (success, messages) = await ChatApi.TryGetMessages(Credential, id, DateTime.MinValue);
        IsTrue(success);
        var enumerable = messages.ToList();
        IsTrue(enumerable.Any());
        IsTrue(enumerable.Any(m => m.Content == Message));
    }
}