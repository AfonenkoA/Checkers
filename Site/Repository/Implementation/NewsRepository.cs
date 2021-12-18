using Api.Interface;
using Common.Entity;
using Site.Data.Models;
using Site.Data.Models.Article;
using Site.Repository.Interface;

namespace Site.Repository.Implementation;

public sealed class NewsRepository : INewsRepository
{
    private readonly IAsyncNewsApi _newsApi;
    private readonly IResourceRepository _resource;

    public NewsRepository(IAsyncNewsApi newsApi, IResourceRepository resource)
    {
        _newsApi = newsApi;
        _resource = resource;
    }

    private Preview ConvertToPreview(ArticleInfo info) =>
        new(info, _resource.GetResource(info.PictureId));

    private VisualArticle ConvertToView(Article info) =>
        new(info, _resource.GetResource(info.PictureId));

    public async Task<(bool, IEnumerable<Preview>)> GetNews()
    {
        var (success, news) = await _newsApi.TryGetNews();
        return (success, news.Select(ConvertToPreview));
    }

    public async Task<(bool, VisualArticle)> GetArticle(int id)
    {
        var (success, article) = await _newsApi.TryGetArticle(id);
        return (success, ConvertToView(article));
    }

    public Task<bool> UpdateTitle(ICredential credential, int id, string title) =>
        _newsApi.UpdateTitle(credential, id, title);

    public Task<bool> UpdateAbstract(ICredential credential, int id, string @abstract) =>
        _newsApi.UpdateAbstract(credential, id, @abstract);

    public Task<bool> UpdateContent(ICredential credential, int id, string content) =>
        _newsApi.UpdateContent(credential, id, content);

    public async Task<bool> UpdatePicture(PictureUpdateData data)
    {
        if (data.Picture == null) return false;
        var (s, id) = await _resource.Create(data, data.Picture);
        if (!s) return false;
        return await _newsApi.UpdatePicture(data, data.Id, id);
    }
}