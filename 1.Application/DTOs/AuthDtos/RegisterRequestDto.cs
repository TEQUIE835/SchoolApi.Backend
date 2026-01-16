using _2.Domain.Entities;

namespace _1.Application.DTOs.AuthDtos;

public class RegisterRequestDto
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    
}