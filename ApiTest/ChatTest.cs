using System;
using System.Linq;
using System.Threading.Tasks;
using Checkers.Api.Interface;
using Checkers.Api.WebImplementation;
using Checkers.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        Assert.IsTrue(getChatSuccess);
        var success = await ChatApi.SendMessage(Credential, id, Message);
        Assert.IsTrue(success);
    }

    [TestMethod]
    public async Task Test02GetFromAllChat()
    {
        var (getChatSuccess, id) = await ChatApi.TryGetCommonChatId(Credential);
        Assert.IsTrue(getChatSuccess);
        var (success, messages) = await ChatApi.TryGetMessages(Credential, id, DateTime.MinValue);
        Assert.IsTrue(success);
        var enumerable = messages.ToList();
        Assert.IsTrue(enumerable.Any());
        Assert.IsTrue(enumerable.Any(m => m.Content == Message));
    }
}