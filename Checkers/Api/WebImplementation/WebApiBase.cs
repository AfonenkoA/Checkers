﻿using System;
using System.Net.Http;
using System.Text.Json;
using Checkers.Api.Interface.Action;
using Checkers.Data.Entity;

namespace Checkers.Api.WebImplementation;

public class WebApiBase
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    protected static T? Deserialize<T>(string s)
    {
        return JsonSerializer.Deserialize<T>(s, Options);
    }
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
    private static string Query(object val) => $"&value={val}";

    protected static string Query(Credential c, int id) => Query(c) + Query(id);
    protected static string Query(ApiAction action, object val) => Query(action) + Query(val);
    protected static string Query(Credential c, ApiAction apiAction, object val) => Query(c, apiAction) + Query(val);
    protected static string Query(Credential c, ApiAction apiAction) => Query(c) + Query(apiAction);
}