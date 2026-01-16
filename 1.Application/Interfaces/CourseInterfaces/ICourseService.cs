using _1.Application.DTOs.CourseDtos;
using _2.Domain.Entities;

namespace _1.Application.Interfaces.CourseInterfaces;

public interface ICourseService
{
    public Task<CourseShowDto?> GetCourseByIdAsync(Guid courseId);
    public Task<ICollection<CourseShowDto>?> GetAllCoursesAsync();
    public Task<CourseShowDto?> AddCourse(CourseCreateDto course);
    public Task<CourseShowDto?> UpdateCourse(CourseCreateDto course);
    public  Task<CourseShowDto?> DeleteCourseByIdAsync(Guid courseId);
    public Task<CourseShowDto?> PublishCourseAsync (Guid courseId);
    public Task<CourseShowDto?> GetCourseWithLessonsByCourseIdAsync(Guid courseId);
    public Task<CourseSummaryDto?> GetCourseSummaryByCourseIdAsync(Guid courseId);
    public Task<CourseShowDto?> UnPublishCourseAsync(Guid courseId);
}