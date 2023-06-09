using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace VeeArc.WebAPI.Authentication;

public class JwtTokenValidator : ISecurityTokenValidator
{
    private readonly string _secretKey;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    
    public bool CanValidateToken => true;
    public int MaximumTokenSizeInBytes { get; set; } = TokenValidationParameters.DefaultMaximumTokenSizeInBytes;
    
    public JwtTokenValidator(string secretKey)
    {
        _secretKey = secretKey;
        _tokenHandler = new JwtSecurityTokenHandler();
    }
    
    public bool CanReadToken(string securityToken)
    {
        return _tokenHandler.CanReadToken(securityToken);
    }

    public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
    {
        byte[] key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenValidationParameters = new TokenValidationParameters
        { 
            ValidateIssuerSigningKey = true, 
            IssuerSigningKey = new SymmetricSecurityKey(key), 
            ValidateIssuer = false, 
            ValidateAudience = false, 
            ValidateLifetime = true
        };

        ClaimsPrincipal claimsPrincipal = _tokenHandler.ValidateToken(securityToken, tokenValidationParameters, out validatedToken);

        return claimsPrincipal;
    }
}
