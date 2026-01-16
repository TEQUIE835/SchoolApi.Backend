using _1.Application.Interfaces.CourseInterfaces;
using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3.Infrastructure.Persistence.Respositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _dbContext;
    public CourseRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Course?> GetCourseByIdAsync(Guid courseId)
    {
        return await _dbContext.Courses.FindAsync(courseId);
    }

    public async Task<ICollection<Course>?> GetAllCoursesAsync()
    {
        return await _dbContext.Courses.ToListAsync();
    }

    public async Task<Course?> UpdateCourse(Course course)
    {
        _dbContext.Courses.Update(course);
        await _dbContext.SaveChangesAsync();
        return course;
    }

    public async Task<Course?> AddCourse(Course course)
    {
        _dbContext.Courses.Add(course);
        await _dbContext.SaveChangesAsync();
        return course;
    }

    public async Task<Course?> GetCourseByIdWithIncludesAsync(Guid courseId)
    {
        return  await _dbContext.Courses.Include(c => c.Lessons).FirstOrDefaultAsync(c => c.Id == courseId);
    }
}