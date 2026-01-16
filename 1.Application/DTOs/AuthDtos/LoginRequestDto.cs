namespace _1.Application.DTOs.AuthDtos;

public class LoginRequestDto
{
    public string UserOrEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
}