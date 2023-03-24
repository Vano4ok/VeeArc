using FluentValidation;

namespace VeeArc.Application.Feature.TestModels.Create;

public class CreateTestModelCommandValidator : AbstractValidator<CreateTestModelCommand>
{
    public CreateTestModelCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(5)
            .NotEmpty();

        RuleFor(v => v.Text)
            .MaximumLength(50)
            .NotEmpty();
    }
}
