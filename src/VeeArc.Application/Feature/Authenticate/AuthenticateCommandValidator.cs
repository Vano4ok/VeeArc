using FluentValidation;
using VeeArc.Application.Common.Interfaces;
using DomainUser = VeeArc.Domain.Entities.User;

namespace VeeArc.Application.Feature.Authenticate;

public class AuthenticateCommandValidator : AbstractValidator<AuthenticateCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;

    public AuthenticateCommandValidator(IUserRepository userRepository, IPasswordHashService passwordHashService)
    {
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;

        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();

        RuleFor(authenticateCommand => authenticateCommand).MustAsync(HaveCorrectCredentials)
            .WithMessage("Username or password is incorrect");
    }

    private async Task<bool> HaveCorrectCredentials(AuthenticateCommand command, CancellationToken cancellationToken)
    {
        DomainUser? user = await _userRepository.GetByUsernameAsync(command.Username);

        if (user is null)
        {
            return false;
        }

        bool isCorrectPassword = _passwordHashService.VerifyHashedPassword(user.Password, command.Password);

        return isCorrectPassword;
    }
}
