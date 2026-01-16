using _1.Application.DTOs.AuthDtos;
using _2.Domain.Entities;

namespace _1.Application.Interfaces.AuthInterfaces;

public interface IAuthService
{
    public Task<RegisterResponseDto?> RegisterAsync(RegisterRequestDto request);
    public Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
    public Task<LoginResponseDto?> RefreshTokenAsync(string refreshToken);
    public Task LogoutAsync(string refreshToken);
}