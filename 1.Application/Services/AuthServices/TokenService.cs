using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using _1.Application.DTOs.AuthDtos;
using _1.Application.Interfaces.AuthInterfaces;
using _2.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace _1.Application.Services.AuthServices;

public class TokenService : ITokenService
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IHashService _hashService;
    public TokenService(IRefreshTokenRepository refreshTokenRepository, IConfiguration configuration, IUserRepository userRepository, IHashService hashService)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _configuration = configuration;
        _userRepository = userRepository;
        _hashService = hashService;
    }
    private string CreateAccessToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // GUID
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                int.Parse(_configuration["Jwt:AccessTokenMinutes"]!)
            ),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string CreateRefreshToken()
    {
        return Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(64)
        );
    }
    private async Task SaveRefreshTokenAsync(Guid userId, string token)
    {
        var refreshToken = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = _hashService.HashPassword(token),
            
        };

        await _refreshTokenRepository.AddRefreshTokenAsync(refreshToken);
        
    }


    public async Task<LoginResponseDto> GenerateTokensAsync(User user)
    {

        var accessToken = CreateAccessToken(user);
        var refreshToken = CreateRefreshToken();


        await SaveRefreshTokenAsync(user.Id, refreshToken);


        return new LoginResponseDto()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public async Task<LoginResponseDto> RefreshTokenAsync(string refreshToken)
    {
        var storedToken = await _refreshTokenRepository.GetRefreshTokenAsync(_hashService.HashPassword(refreshToken));

        if (storedToken == null)
            throw new UnauthorizedAccessException("Invalid refresh token");

        if (storedToken.IsExpired)
            throw new UnauthorizedAccessException("Refresh token expired or revoked");

        var user = await _userRepository.GetUserByIdAsync(storedToken.UserId);

        if (user == null)
            throw new UnauthorizedAccessException("User not found");

        var newAccessToken = CreateAccessToken(user);

        return new LoginResponseDto()
        {
            AccessToken = newAccessToken,
            RefreshToken = refreshToken
        };
    }
}