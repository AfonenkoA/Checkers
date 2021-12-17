using static System.String;

namespace Site.Data.Models;

public class Caller
{
    public string CallerController { get; set; } = Empty;
    public string CallerAction { get; set; } = Empty;

    public Caller()
    { }

    public Caller(string controller, string action)
    {
        CallerController = controller;
        CallerAction = action;
    }
}