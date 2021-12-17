﻿using Api.Interface;
using Common.Entity;
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
}