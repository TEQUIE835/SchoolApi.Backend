using _2.Domain.Entities;
using _3.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace _3.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(this IServiceProvider services)
    {
        var context = services.GetRequiredService<AppDbContext>();

        // Verificar si ya existe un admin
        if (await context.Users.AnyAsync(u => u.Role == UserRole.Admin))
            return;

        var passwordHash = BCrypt.Net.BCrypt.HashPassword("Admin123*");

        var adminUser = new User
        {
            Id = Guid.NewGuid(),
            Username = "admin",
            Email = "admin@school.com",
            Password = passwordHash,
            Role = UserRole.Admin,
        };

        context.Users.Add(adminUser);
        await context.SaveChangesAsync();
    }
}