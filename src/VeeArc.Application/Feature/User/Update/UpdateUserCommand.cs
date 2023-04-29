using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using VeeArc.Application.Common.Interfaces;
using DomainUser = VeeArc.Domain.Entities.User;

namespace VeeArc.Application.Feature.User.Update;

[Authorize]
public record UpdateUserCommand : IRequest<UserResponse>
{
    public required int Id { get; init; }
    
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Email { get; init; } 
    
    public string Password { get; init; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUserRepository userRepository,
                                    IPasswordHashService passwordHashService,
                                    IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHashService = passwordHashService;
        _mapper = mapper;
    }
    
    public async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        DomainUser? user = await _userRepository.GetByIdAsync(request.Id);

        TrySetEmail(request.Email, user);

        TrySetFirstname(request.FirstName, user);
        
        TrySetLastname(request.LastName, user);
        
        TrySetPassword(request.Password, user);

        _userRepository.Update(user);
        
        await _userRepository.SaveAsync();

        UserResponse response = _mapper.Map<UserResponse>(user);

        return response;
    }
    
    private void TrySetEmail(string email, DomainUser user)
    {
        if (!string.IsNullOrEmpty(email))
        {
            user.Email = email;
        }
    }
    
    private void TrySetFirstname(string firstname, DomainUser user)
    {
        if (!string.IsNullOrEmpty(firstname))
        {
            user.FirstName = firstname;
        }
    }
    
    private void TrySetLastname(string lastname, DomainUser user)
    {
        if (!string.IsNullOrEmpty(lastname))
        {
            user.LastName = lastname;
        }
    }
    
    private void TrySetPassword(string password, DomainUser user)
    {
        if (!string.IsNullOrEmpty(password))
        {
            string hashedPassword = _passwordHashService.Hash(password);
            
            user.Password = hashedPassword;
        }
    }
}
