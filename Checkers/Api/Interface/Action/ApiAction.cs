namespace Checkers.Api.Interface.Action;

public class ApiAction
{
    private readonly string name;

    protected ApiAction(string name)
    {
        this.name = name;
    }

    public override string ToString()
    {
        return name;
    }
}