namespace Site.Data.Models;

public class ResourceView
{
    public ResourceView(string url)
    {
        Url = url;
    }

    public ResourceView(ResourceView resource)
    {
        Url = resource.Url;
    }

    public string Url { get; }


}