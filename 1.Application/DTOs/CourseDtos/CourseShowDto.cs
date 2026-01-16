namespace _1.Application.DTOs.CourseDtos;

public class CourseShowDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } 
    public string Status { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}