namespace _2.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
    public List<RefreshToken> RefreshTokens { get; set; }
}

public enum UserRole
{
    Admin,
    User
}