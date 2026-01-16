using _2.Domain.Entities;

namespace _1.Application.Interfaces.AuthInterfaces;

public interface IUserRepository
{
    public Task<User?> GetUserByEmailAsync(string email);
    public Task<User?> GetUserByIdAsync(Guid userId);
    public Task<User?> GetUserByUsernameAsync(string username);
    public Task<User?> UpdateUserAsync(User user);
    public Task<User?> CreateUserAsync(User user);
}