namespace _2.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; } =  DateTime.UtcNow.AddDays(30);
    public bool IsExpired => DateTime.UtcNow > Expires;
}