using MediatR;
using VeeArc.Application.Common.Exceptions;
using VeeArc.Application.Common.Interfaces;
using VeeArc.Domain.Entities;

namespace VeeArc.Application.Feature.Articles.Delete;

public record DeleteArticleCommand : IRequest
{
    public required int Id { get; init; }
}

public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleStorageRepository _articleStorageRepository;

    public DeleteArticleCommandHandler(
        IArticleRepository articleRepository,
        IArticleStorageRepository articleStorageRepository)
    {
        _articleRepository = articleRepository;
        _articleStorageRepository = articleStorageRepository;
    }

    public async Task Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        Article? article = await _articleRepository.GetByIdAsync(request.Id);

        if (article is null)
        {
            throw new NotFoundException(nameof(Article), request.Id);
        }

        await _articleStorageRepository.DeleteArticle(article.FileLink, cancellationToken);

        _articleRepository.Remove(article);

        await _articleRepository.SaveAsync();
    }
}
