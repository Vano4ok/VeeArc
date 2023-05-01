using FluentValidation;
using VeeArc.Application.Common.Interfaces;
using DomainUser = VeeArc.Domain.Entities.User;

namespace VeeArc.Application.Feature.User.Delete;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        
        RuleFor(user => user.Id).NotEmpty()
            .MustAsync(BeExistingUser)
            .WithMessage("User with given id does not exist");
    }

    private async Task<bool> BeExistingUser(int id, CancellationToken cancellationToken)
    {
        DomainUser? user = await _userRepository.GetByIdAsync(id);

        return user is not null;
    }
}
