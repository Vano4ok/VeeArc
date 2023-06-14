using FluentValidation;
using VeeArc.Application.Common.Interfaces;
using DomainUser = VeeArc.Domain.Entities.User;

namespace VeeArc.Application.Feature.User.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(user => user.FirstName).NotEmpty()
            .MaximumLength(25);

        RuleFor(user => user.LastName).NotEmpty()
            .MaximumLength(45);

        RuleFor(user => user.Password).NotEmpty()
            .MinimumLength(8)
            .MaximumLength(200);

        RuleFor(user => user.Email).NotEmpty()
            .EmailAddress()
            .MustAsync(BeUniqueEmail)
            .WithMessage("Email is already used");

        RuleFor(user => user.Username).NotEmpty()
            .MinimumLength(4)
            .MustAsync(BeUniqueUsername)
            .WithMessage("Username is already used");
    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        DomainUser? user = await _userRepository.GetByEmailAsync(email);

        return user is null;
    }

    private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
    {
        DomainUser? user = await _userRepository.GetByUsernameAsync(username);

        return user is null;
    }
}
