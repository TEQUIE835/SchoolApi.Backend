namespace _1.Application.DTOs.CourseDtos;

public class CourseSummaryDto
{
    public Guid CourseId { get; set; }
    public string CourseTitle { get; set; }
    public int LessonsCount { get; set; }
    public DateTime UpdatedAt { get; set; }
}