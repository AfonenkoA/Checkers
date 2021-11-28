namespace Checkers.Api.Interface.Action;

public class ApiAction
{
    private readonly string _name;

    protected ApiAction(string name)
    {
        _name = name;
    }

    public override string ToString()
    {
        return _name;
    }
}