using AutoMapper;
using MediatR;
using VeeArc.Application.Common.Interfaces;
using VeeArc.Application.Common.Mappings;

namespace VeeArc.Application.Feature.User.Create;

public class CreateUserCommand : IRequest<UserResponse>
{
    public required string Username { get; init; }
    
    public required string FirstName { get; init; }
    
    public required string LastName { get; init; }
    
    public required string Email { get; init; }

    public required string Password { get; init; }
}


public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponse>
{ 
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHasher;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepository userRepository,
                                    IPasswordHashService passwordHasher,
                                    IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<UserResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
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

        UserResponse response = _mapper.Map<UserResponse>(user);
        
        return response;
    }
}