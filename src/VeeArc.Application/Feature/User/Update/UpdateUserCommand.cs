using System.Linq.Expressions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using VeeArc.Application.Common.Interfaces;
using DomainUser = VeeArc.Domain.Entities.User;

namespace VeeArc.Application.Feature.User.Update;

[Authorize]
public record UpdateUserCommand : IRequest<UserResponse>
{
    public required int Id { get; init; }
    
    public required string FirstName { get; init; }

    public required string LastName { get; init; }

    public required string Email { get; init; } 
    
    public required string Password { get; init; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;
    
    public UpdateUserCommandHandler(IUserRepository userRepository, IPasswordHashService passwordHashService)
    {
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
    }
    
    public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        DomainUser? user = await _userRepository.GetByIdAsync(request.Id);

        if (!string.IsNullOrEmpty(request.Email))
        {
            user.Email = request.Email;
        }

        if (!string.IsNullOrEmpty(request.FirstName))
        {
            user.FirstName = request.FirstName;
        }
        
        if(!string.IsNullOrEmpty(request.LastName))
        {
            user.LastName = request.LastName;
        }
        
        if(!string.IsNullOrEmpty(request.Password))
        {
            string hashedPassword = _passwordHashService.Hash(request.Password);
            
            user.Password = hashedPassword;
        }
        
        _userRepository.Update(user);
        
        await _userRepository.SaveAsync();

        return user;
    }
}