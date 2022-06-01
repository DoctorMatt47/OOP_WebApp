using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace OOP_WebApp.Application.Users;

public class HashService : IHashService
{
    private const int BitCount = 8;
    private const int IterationCount = 100_000;
    private const int SubkeyBitCount = 256;

    public byte[] GenerateSalt()
    {
        var salt = new byte[16];
        RandomNumberGenerator.Create().GetBytes(salt);
        return salt;
    }

    public string HashPassword(string password, byte[] salt) =>
        Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                password,
                salt,
                KeyDerivationPrf.HMACSHA256,
                IterationCount,
                SubkeyBitCount / BitCount));
}