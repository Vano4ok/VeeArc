using MediatR;
using VeeArc.Application.Common.Interfaces;

namespace VeeArc.Application.Feature.User.Create;

public class CreateUserCommand : IRequest<Domain.Entities.User>
{
    public required string Username { get; init; }
    
    public required string FirstName { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Email { get; init; }

    public required string Password { get; init; }
}


public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Domain.Entities.User>
{ 
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHasher;
    
    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHashService passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Domain.Entities.User> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        string hashedPassword = _passwordHasher.Hash(command.Password);
        
        var user = new Domain.Entities.User
        {
            Username = command.Username,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = hashedPassword
        };

        await _userRepository.AddAsync(user);
        
        await _userRepository.SaveAsync();

        return user;
    }
}