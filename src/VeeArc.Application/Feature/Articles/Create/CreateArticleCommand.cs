using AutoMapper;
using MediatR;
using VeeArc.Application.Common.Interfaces;
using VeeArc.Domain.Entities;
using VeeArc.Domain.Enums;

namespace VeeArc.Application.Feature.Articles.Create;

public record CreateArticleCommand : IRequest<ArticleResponse>
{
    public required string Title { get; init; }

    public required string Text { get; init; }
}

public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ArticleResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleStorageRepository _articleStorageRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public CreateArticleCommandHandler(
        IArticleRepository articleRepository,
        IArticleStorageRepository articleStorageRepository,
        ICurrentUserService currentUserService,
        IMapper mapper)
    {
        _articleRepository = articleRepository;
        _articleStorageRepository = articleStorageRepository;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<ArticleResponse> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        string articleFileLink = await _articleStorageRepository.UploadArticle(request.Text, cancellationToken);

        var article = new Article
        {
            Title = request.Title,
            FileLink = articleFileLink,
            State = ArticleState.Draft,
            UserId = _currentUserService.UserId.Value,
        };

        await _articleRepository.AddAsync(article);
        await _articleRepository.SaveAsync();

        ArticleResponse response = _mapper.Map<ArticleResponse>(article);

        return response;
    }


}
