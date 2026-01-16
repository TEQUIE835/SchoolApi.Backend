using _1.Application.DTOs.AuthDtos;
using _1.Application.DTOs.CourseDtos;
using _1.Application.Interfaces.AuthInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        try
        {
            var user = await _authService.RegisterAsync(request);
            if (user == null) return BadRequest();
            return Ok(user);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }   
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var response = await _authService.LoginAsync(request);
            if (response == null) return BadRequest();
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [HttpPost("refreshtoken")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        try
        {
            var response =  await _authService.RefreshTokenAsync(refreshToken);
            if (response == null) return BadRequest();
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [Authorize]
    [HttpDelete("logout")]
    public async Task<IActionResult> Logout([FromBody] string refreshToken)
    {
        try
        {
            await _authService.LogoutAsync(refreshToken);
            return Ok("success");
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }
}