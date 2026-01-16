using _2.Domain.Entities;

namespace _1.Application.Interfaces.AuthInterfaces;

public interface IRefreshTokenRepository
{
    public Task<RefreshToken?> GetRefreshTokenAsync(string token);
    public Task<RefreshToken?> AddRefreshTokenAsync(RefreshToken refreshToken);
    public Task DeleteRefreshTokenAsync(RefreshToken refreshToken);
}