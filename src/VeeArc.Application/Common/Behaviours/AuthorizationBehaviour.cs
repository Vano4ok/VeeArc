using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using VeeArc.Application.Common.Exceptions;
using VeeArc.Application.Common.Interfaces;
using VeeArc.Domain.Entities;

namespace VeeArc.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUserRepository _userRepository;

    public AuthorizationBehaviour(ICurrentUserService currentUserService, IUserRepository userRepository)
    {
        _currentUserService = currentUserService;
        _userRepository = userRepository;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>().ToList();

        if (!authorizeAttributes.Any())
        {
            return await next();
        }
        
        if (_currentUserService.UserId == null)
        {
            throw new UnauthorizedAccessException();
        }
        
        List<string> roles = authorizeAttributes.Where(a => a.Roles is not null)
                                                .SelectMany(a => a.Roles.Split(',')
                                                .Select(r => r.Trim()))
                                                .ToList();
        
        if (!roles.Any())
        {
            return await next();
        }
        
        bool isUserInRoles = await IsUserInRoles(_currentUserService.UserId.Value, roles);

        if (isUserInRoles)
        {
            return await next();
        }

        throw new ForbiddenAccessException();
    }

    private async Task<bool> IsUserInRoles(int userId, List<string> roles)
    {
        User? user = await _userRepository.GetByIdAsync(userId);

        return user.Roles.Any(role => roles.Contains(role.Name));
    }
}