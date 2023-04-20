using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using VeeArc.Application.Common.Interfaces;

namespace VeeArc.Infrastructure.BlobStorages;

public class ArticleStorageRepository : IArticleStorageRepository
{
    private const string StorageContainerName = "articles";

    private readonly BlobContainerClient _blobContainerClient;

    public ArticleStorageRepository(BlobServiceClient blobServiceClient)
    {
        _blobContainerClient = blobServiceClient.GetBlobContainerClient(StorageContainerName);
    }

    public async Task<string> UploadArticle(string articleText, CancellationToken cancellationToken)
    {
        string articleFileName = CreateArticleFileName();

        BlobClient blobClient = _blobContainerClient.GetBlobClient(articleFileName);

        await blobClient.UploadAsync(BinaryData.FromString(articleText), cancellationToken);

        string articleFileUri = CreateArticleFileUri(articleFileName);

        return articleFileUri;
    }

    public async Task DeleteArticle(string atricleFileUrl, CancellationToken cancellationToken)
    {
        string articleFileName = GetArticleFileName(atricleFileUrl);

        await _blobContainerClient.DeleteBlobAsync(articleFileName, cancellationToken: cancellationToken);
    }

    internal static void Init(BlobServiceClient blobServiceClient)
    {
        BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(StorageContainerName);

        blobContainerClient.CreateIfNotExists(PublicAccessType.BlobContainer);
    }

    private static string CreateArticleFileName()
    {
        string articleFileName = Guid.NewGuid().ToString() + ".txt";

        return articleFileName;
    }

    private string CreateArticleFileUri(string articleFileName)
    {
        string articleFileUri = _blobContainerClient.Uri.AbsoluteUri + '/' + articleFileName;

        return articleFileUri;
    }

    private static string GetArticleFileName(string atricleFileUrl)
    {
        string articleFileName = atricleFileUrl
            .Split('/')
            .Last();

        return articleFileName;
    }
}
