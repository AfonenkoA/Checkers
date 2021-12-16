using Api.Interface;
using Common.Entity;
using Site.Data.Models.Article;
using Site.Repository.Interface;

namespace Site.Repository.Implementation;

public class NewsRepository : INewsRepository
{
    private readonly IAsyncNewsApi _newsApi;
    private readonly IAsyncResourceService _resourceService;

    public NewsRepository(IAsyncNewsApi newsApi, IAsyncResourceService resourceService)
    {
        _newsApi = newsApi;
        _resourceService = resourceService;
    }

    private ArticlePreview ConvertToPreview(ArticleInfo info) =>
        new(info, _resourceService.GetFileUrl(info.PictureId));

    private ArticleView ConvertToView(Article info) =>
        new(info, _resourceService.GetFileUrl(info.PictureId));

    public async Task<(bool, IEnumerable<ArticlePreview>)> GetNews()
    {
        var (success, news) = await _newsApi.TryGetNews();
        return (success, news.Select(ConvertToPreview));
    }

    public async Task<(bool, ArticleView)> GetArticle(int id)
    {
        var (success, article) = await _newsApi.TryGetArticle(id);
        return (success, ConvertToView(article));
    }
}