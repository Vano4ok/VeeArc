using AutoMapper;
using MediatR;
using VeeArc.Application.Common.Interfaces;
using VeeArc.Domain.Entities;

namespace VeeArc.Application.Feature.Articles.GetAll;

public record GetArticlesQuery : IRequest<List<ArticleResponse>>
{

}

public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, List<ArticleResponse>>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IMapper _mapper;

    public GetArticlesQueryHandler(IArticleRepository articleRepository, IMapper mapper)
    {
        _articleRepository = articleRepository;
        _mapper = mapper;
    }

    public async Task<List<ArticleResponse>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
    {
        List<Article> articles = await _articleRepository.GetAll(cancellationToken);

        List<ArticleResponse> response = _mapper.Map<List<Article>, List<ArticleResponse>>(articles);

        return response;
    }
}