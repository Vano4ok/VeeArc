using AutoMapper;
using MediatR;
using VeeArc.Application.Common.Exceptions;
using VeeArc.Application.Common.Interfaces;
using VeeArc.Domain.Entities;

namespace VeeArc.Application.Feature.Articles.Get;

public record GetArticleQuery : IRequest<ArticleResponse>
{
    public required int Id { get; init; }
}

public class GetArticleQueryHandler : IRequestHandler<GetArticleQuery, ArticleResponse>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetArticleQueryHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<ArticleResponse> Handle(GetArticleQuery request, CancellationToken cancellationToken)
    {
        Article article = await _articleRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Article), request.Id);

        ArticleResponse response = _mapper.Map<ArticleResponse>(article);

        return response;
    }
}
