using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using VeeArc.Application.Common.Interfaces;

namespace VeeArc.Application.Common.Services;

public class PasswordHashService : IPasswordHashService
{
    private const int SaltSize = 16;

    private const int KeySize = 32;
    
    private const int Iterations = 100_000;
    
    public string Hash(string password)
    {
        using var algorithm = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA512);
        
        string key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
        string salt = Convert.ToBase64String(algorithm.Salt);

        return string.Join(".", Iterations, salt, key);
    }

    public bool VerifyHashedPassword(string hashedPassword, string password)
    {
        string[] parts = hashedPassword.Split('.', 3);

        if (parts.Length != 3)
        {
            throw new FormatException("Unexpected hash format. " +
                                      "Should be formatted as `{iterations}.{salt}.{hash}`");
        }

        int iterations = Convert.ToInt32(parts[0]);
        byte[] salt = Convert.FromBase64String(parts[1]);
        byte[] key = Convert.FromBase64String(parts[2]);

        using var algorithm = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA512);
        
        byte[] keyToCheck = algorithm.GetBytes(KeySize);
        bool verified = keyToCheck.SequenceEqual(key);

        return verified;
    }
}
