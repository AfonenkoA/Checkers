using ApiContract.Action;
using Common.Entity;

namespace Api.WebImplementation;

public class WebApiBase
{
    protected static readonly HttpClient Client = new() { BaseAddress = new Uri("http://localhost:5005/api/") };

    protected static string Query(Credential c) => $"?login={c.Login}&password={c.Password}";
    private static string Query(ApiAction action) => $"&action={action}";
    private static string Query(string val) => $"&val={val}";
    protected static string Query(Credential c, string val) => Query(c) + Query(val);
    protected static string QueryAction(ApiAction action) => $"?action={action}";
    protected static string Query(Credential c, ApiAction apiAction) => Query(c) + Query(apiAction);
}