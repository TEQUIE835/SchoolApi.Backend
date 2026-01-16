using _1.Application.DTOs.AuthDtos;
using _2.Domain.Entities;

namespace _1.Application.Interfaces.AuthInterfaces;

public interface ITokenService
{
    Task<LoginResponseDto> GenerateTokensAsync(User user);

    Task<LoginResponseDto> RefreshTokenAsync(string refreshToken);
}