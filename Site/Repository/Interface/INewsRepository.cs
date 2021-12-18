using Common.Entity;
using Site.Data.Models;
using Site.Data.Models.Article;

namespace Site.Repository.Interface;

public interface INewsRepository
{
    public Task<(bool, IEnumerable<Preview>)> GetNews();
    public Task<(bool,VisualArticle)> GetArticle(int id);

    Task<bool> UpdateTitle(ICredential credential, int id, string title);
    Task<bool> UpdateAbstract(ICredential credential, int id, string @abstract);
    Task<bool> UpdateContent(ICredential credential, int id, string content);
    Task<bool> UpdatePicture(PictureUpdateData data);
}