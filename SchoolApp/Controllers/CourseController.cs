
using _1.Application.DTOs.CourseDtos;
using _1.Application.Interfaces.CourseInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SchoolApp.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet("{courseId:guid}")]
    public async Task<IActionResult> GetCourseAsync(Guid courseId)
    {
        try
        {
            var course = await _courseService.GetCourseWithLessonsByCourseIdAsync(courseId);
            return Ok(course);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllCoursesAsync()
    {
        try
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateCourseAsync([FromBody] CourseCreateDto courseCreateDto)
    {
        try
        {
            var course = await _courseService.AddCourse(courseCreateDto);
            return Ok(course);
        }
        catch (Exception e)
        {
            return BadRequest("Error: "  + e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{courseId:guid}/publish")]
    public async Task<IActionResult> PublishCourseAsync(Guid courseId)
    {
        try
        {
            var course = await _courseService.PublishCourseAsync(courseId);
            return Ok(course);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPatch("{courseId:guid}/unpublish")]
    public async Task<IActionResult> UnPublishCourseAsync(Guid courseId)
    {
        try
        {
            var course = await _courseService.UnPublishCourseAsync(courseId);
            return Ok(course);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{courseId:guid}/delete")]
    public async Task<IActionResult> DeleteCourseAsync(Guid courseId)
    {
        try
        {
            await _courseService.DeleteCourseByIdAsync(courseId);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{courseId:guid}/summary")]
    public async Task<IActionResult> GetCourseSummaryAsync(Guid courseId)
    {
        try
        {
            var response = await _courseService.GetCourseSummaryByCourseIdAsync(courseId);
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest("Error: " + e.Message);
        }
    }
}