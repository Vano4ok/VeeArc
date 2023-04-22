using FluentValidation;

namespace VeeArc.Application.Feature.Articles.Create;

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    public CreateArticleCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(v => v.Text)
            .NotEmpty().WithMessage("Text is required.")
            .MaximumLength(30000).WithMessage("Text must not exceed 30000 characters.");
    }
}
