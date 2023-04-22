using VeeArc.Application.Common.Mappings;
using VeeArc.Domain.Entities;
using VeeArc.Domain.Enums;

namespace VeeArc.Application.Feature.Articles;

public class ArticleResponse : IMapFrom<Article>
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string FileLink { get; set; }

    public ArticleState State { get; set; }

    public DateTime? LastChanged { get; set; }

    public DateTime CreatedAt { get; set; }
}
