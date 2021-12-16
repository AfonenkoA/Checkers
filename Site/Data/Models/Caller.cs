namespace Site.Data.Models;

public class Caller
{
    public string Controller { get; }
    public string Action { get; }

    public Caller(string controller, string action)
    {
        Controller = controller;
        Action = action;
    }
}