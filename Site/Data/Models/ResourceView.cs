using Common.Entity;

namespace Site.Data.Models;

public class ResourceView
{
    public static readonly ResourceView Invalid = new(EntityValues.InvalidString);
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