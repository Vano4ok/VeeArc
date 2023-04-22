namespace VeeArc.Application.Common.Interfaces;

public interface IArticleStorageRepository
{
    Task<string> UploadArticle(string article, CancellationToken cancellationToken);

    Task DeleteArticle(string atricleFileUrl, CancellationToken cancellationToken);
}
