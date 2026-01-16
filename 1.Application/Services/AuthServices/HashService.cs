using _1.Application.Interfaces.AuthInterfaces;

namespace _1.Application.Services.AuthServices;

public class HashService :IHashService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        return BCrypt.Net.BCrypt.Verify(hashedPassword, password);
    }
}