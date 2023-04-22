using FluentValidation;

namespace VeeArc.Application.Feature.Articles.Get;

public class GetArticleQueryValidator : AbstractValidator<GetArticleQuery>
{
    public GetArticleQueryValidator()
    {
        RuleFor(v => v.Id)
            .GreaterThanOrEqualTo(0);
    }
}
