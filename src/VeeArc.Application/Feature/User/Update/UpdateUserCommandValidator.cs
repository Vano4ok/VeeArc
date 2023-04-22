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
        
        RuleFor(user => user.Id).MustAsync(BeExistingUser)
            .WithMessage("User with given id does not exist");
        
        RuleFor(user => user.FirstName)
            .MaximumLength(25)
            .NotEmpty();

        RuleFor(user => user.LastName)
            .MaximumLength(45)
            .NotEmpty();

        RuleFor(user => user.Email)
            .EmailAddress()
            .NotEmpty();

        RuleFor(user => user.Password)
            .MaximumLength(200)
            .Must(x => string.IsNullOrEmpty(x) || x.Length >= 8)
            .WithMessage("Password must be at least 8 characters long");
    }

    private async Task<bool> BeExistingUser(int id, CancellationToken cancellationToken)
    {
       DomainUser? user = await _userRepository.GetByIdAsync(id);
       
        return user is not null;
    }
}