using Microsoft.EntityFrameworkCore.Query.Internal;

namespace VeeArc.Application.Common.Interfaces;

public interface IPasswordHashService
{
    public string Hash(string password);
    
    public bool VerifyHashedPassword(string hashedPassword, string password);
}
