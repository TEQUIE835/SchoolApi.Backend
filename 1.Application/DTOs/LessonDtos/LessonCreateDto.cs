namespace _1.Application.DTOs.LessonDtos;

public class LessonCreateDto
{
    public Guid LessonId { get; set; }
    public Guid CourseId { get; set; }
    public string Title { get; set; }
    public int Order { get; set; }
}