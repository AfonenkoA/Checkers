namespace Site.Data.Models;

public class PictureView
{
    public PictureView(string pictureUrl)
    {
        PictureUrl = pictureUrl;
    }

    public string PictureUrl { get; }
}