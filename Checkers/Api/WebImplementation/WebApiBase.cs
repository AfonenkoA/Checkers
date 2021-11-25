using System;
using System.Net.Http;
using Checkers.Data.Entity;
using static System.Text.Json.JsonSerializer;

namespace Checkers.Api.WebImplementation;



public class WebApiBase
{
    protected static readonly HttpClient Client = new(){BaseAddress = new Uri("http://localhost:5005/api/")};
    public const string UserRoute = "newuser";
    public const string ItemRoute = "item";
    public const string StatisticsRoute = "stat";
    public const string ChatRoute = "chat";
    public const string NewsRoute = "news";
    public const string ForumRoute = "forum";
    public const string ResourceRoute = "res";
    public const string GameRoute = "game";

    protected static string Query(Credential c) => $"?credential={Serialize(c)}";
    protected static string Query(ApiAction action) => $"&action={action}";
    protected static string Query(object val) => $"&value={val}";

    protected static string Query(Credential c, int id) => Query(c) + Query(id);
    protected static string Query(ApiAction action, object val) => Query(action) + Query(val);
    protected static string Query(Credential c, ApiAction apiAction, object val) => Query(c, apiAction) + Query(val);
    protected static string Query(Credential c, ApiAction apiAction) => Query(c) + Query(apiAction);
}