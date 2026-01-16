namespace _2.Domain.Entities;

public class Course
{
    public Guid Id { get; set; }
    public string Title { get; set; } 
    public CourseStatus Status { get; set; } = CourseStatus.Draft;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public List<Lesson> Lessons { get; set; } = new List<Lesson>();
}

public enum CourseStatus
{
    Draft,
    Published
}