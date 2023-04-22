namespace VeeArc.Application.Common.Settings;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    
    public required string SecretKey { get; init; }
    
    public required int TokenLifeTimeInHours { get; init; }
}