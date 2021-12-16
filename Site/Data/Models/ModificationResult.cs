namespace Site.Data.Models;

public class ModificationResult : Caller
{
    public bool Success { get; }
    public ModificationResult(string controller, string action, bool success) : base(controller, action)
    {
        Success = success;
    }
}