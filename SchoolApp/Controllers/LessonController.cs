using _1.Application.DTOs.LessonDtos;
using _1.Application.Interfaces.LessonInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolApp.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class LessonController : ControllerBase
{
    private readonly ILessonService _lessonService;

    public LessonController(ILessonService lessonService)
    {
        _lessonService = lessonService;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var response =  await _lessonService.GetAllLessonsAsync();
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var response = await _lessonService.GetLessonByIdAsync(id);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateLesson(LessonCreateDto lesson)
    {
        try
        {
            var response = await _lessonService.AddLesson(lesson);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> DeleteLesson(Guid id)
    {
        try
        {
            var response = await _lessonService.DeleteLessonByIdAsync(id);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }
}