using _2.Domain.Entities;

namespace _1.Application.Interfaces.CourseInterfaces;

public interface ICourseRepository
{
    public Task<Course?> GetCourseByIdAsync(Guid courseId);
    public Task<ICollection<Course>?> GetAllCoursesAsync();
    public Task<Course?> UpdateCourse(Course course);
    public Task<Course?> AddCourse(Course course);
    public Task<Course?> GetCourseByIdWithIncludesAsync(Guid courseId);
    
}