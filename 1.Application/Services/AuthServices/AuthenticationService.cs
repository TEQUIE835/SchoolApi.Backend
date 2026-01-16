using _1.Application.DTOs.AuthDtos;
using _1.Application.Interfaces.AuthInterfaces;
using _2.Domain.Entities;

namespace _1.Application.Services.AuthServices;

public class AuthenticationService :IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IHashService _hashService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AuthenticationService(IUserRepository userRepository,ITokenService tokenService, IHashService hashService, IRefreshTokenRepository refreshTokenRepository)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _hashService = hashService;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<RegisterResponseDto?> RegisterAsync(RegisterRequestDto request)
    {
        var user = new User()
        {
            Username = request.Username,
            Password = _hashService.HashPassword(request.Password),
            Email = request.Email
        };
        var res = await _userRepository.CreateUserAsync(user);
        var response = new RegisterResponseDto()
        {
            UserId = res.Id,
            Username = res.Username,
        };
        return response;
    }

    public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetUserByEmailAsync(request.UserOrEmail);
        if (user == null)
        {
            user = await  _userRepository.GetUserByUsernameAsync(request.UserOrEmail);
            if (user == null) throw new ArgumentException("Invalid username or password");
        }
        var valid = _hashService.VerifyPassword(user.Password, request.Password);
        if (!valid) throw new ArgumentException("Invalid username or password");
        var response = await _tokenService.GenerateTokensAsync(user);
        return response;
    }

    public async Task<LoginResponseDto?> RefreshTokenAsync(string refreshToken)
    {
        var response =  await _tokenService.RefreshTokenAsync(refreshToken);
        return response;
    }

    public async Task LogoutAsync(string refreshToken)
    {
        var token = await _refreshTokenRepository.GetRefreshTokenAsync(refreshToken);
        if (token == null) throw new ArgumentException("Invalid refresh token");
        await _refreshTokenRepository.DeleteRefreshTokenAsync(token);
    }
}