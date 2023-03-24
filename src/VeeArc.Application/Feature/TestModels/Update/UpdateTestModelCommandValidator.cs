using FluentValidation;

namespace VeeArc.Application.Feature.TestModels.Update;

public class UpdateTestModelCommandValidator : AbstractValidator<UpdateTestModelCommand>
{
    public UpdateTestModelCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(5)
            .NotEmpty();

        RuleFor(v => v.Text)
            .MaximumLength(50)
            .MaximumLength(2);

        RuleFor(v => v.PageIndex)
            .GreaterThan(5)
            .When(v => v.PageIndex.HasValue);
    }
}