using System;
using System.Net.Http;
using Checkers.Api.Interface.Action;
using Checkers.Data.Entity;

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

    protected static string Query(Credential c) => $"?login={c.Login}&password={c.Password}";
    private static string Query(ApiAction action) => $"&action={action}";
    private static string Query(string val) => $"&val={val}";
    protected static string Query(Credential c, string val) => Query(c) + Query(val);
    public static string QueryAction(ApiAction action) => $"?action={action}";
    protected static string Query(Credential c, ApiAction apiAction) => Query(c) + Query(apiAction);
}