using _1.Application.DTOs.AuthDtos;
using _2.Domain.Entities;


namespace _1.Application.Interfaces.AuthInterfaces;

public interface IHashService
{
    public string HashPassword(string password);
    public bool VerifyPassword(string hashedPassword, string password);
}