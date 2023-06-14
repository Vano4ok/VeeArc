using FluentValidation;
using VeeArc.Application.Common.Interfaces;
using DomainUser = VeeArc.Domain.Entities.User;

namespace VeeArc.Application.Feature.User.Update;

public class UpdateUserModelCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserModelCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        
        RuleFor(user => user.Id).GreaterThanOrEqualTo(0)
            .MustAsync(BeExistingUser)
            .WithMessage("User with given id does not exist");
        
        RuleFor(user => user.FirstName)
            .MaximumLength(25)
            .NotEmpty()
            .When(user => !string.IsNullOrEmpty(user.FirstName));
        
        RuleFor(user => user.LastName)
            .MaximumLength(45)
            .NotEmpty()
            .When(user => !string.IsNullOrEmpty(user.LastName));

        RuleFor(user => user.Email)
            .EmailAddress()
            .NotEmpty()
            .When(user => !string.IsNullOrEmpty(user.Email));

        RuleFor(user => user.Password)
            .MaximumLength(200)
            .MinimumLength(8)
            .When(user => !string.IsNullOrEmpty(user.Password))
            .WithMessage("Password must be at least 8 characters long");
    }

    private async Task<bool> BeExistingUser(int id, CancellationToken cancellationToken)
    {
       DomainUser? user = await _userRepository.GetByIdAsync(id);
       
        return user is not null;
    }
}
