using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VeeArc.Application.Common.Interfaces;
using VeeArc.Application.Common.Settings;
using DomainUser = VeeArc.Domain.Entities.User;

namespace VeeArc.Application.Feature.Authenticate;

public class AuthenticateCommand : IRequest<Jwt>
{
    public string Username { get; set; }

    public string Password { get; set; }
}

public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, Jwt>
{
    private readonly JwtOptions _jwtOptions;
    private readonly IUserRepository _userRepository;

    public AuthenticateCommandHandler(IUserRepository userRepository, IOptions<JwtOptions> jwtOptions)
    {
        _userRepository = userRepository;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<Jwt> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
    {
        DomainUser? user = await _userRepository.GetByUsernameAsync(request.Username);

        var tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(_jwtOptions.SecretKey);

        var claims = new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email)
        });

        DateTime expireDate = DateTime.UtcNow.AddHours(_jwtOptions.TokenLifeTimeInHours);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claims,
            Expires = expireDate,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return new Jwt
        {
            Token = tokenHandler.WriteToken(token),
            ExpDate = expireDate
        };
    }
}
