using _1.Application.Interfaces.LessonInterfaces;
using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3.Infrastructure.Persistence.Respositories;

public class LessonRepository : ILessonRepository
{
    private readonly AppDbContext _dbContext;
    public LessonRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Lesson?> GetLessonByIdAsync(Guid lessonId)
    {
        return await _dbContext.Lessons.FindAsync(lessonId);
    }

    public async Task<ICollection<Lesson>?> GetAllLessonsAsync()
    {
        return await _dbContext.Lessons.ToListAsync();
    }

    public async Task<Lesson?> AddLesson(Lesson lesson)
    {
        _dbContext.Lessons.Add(lesson);
        await _dbContext.SaveChangesAsync();
        return lesson;
    }

    public async Task<Lesson?> UpdateLesson(Lesson lesson)
    {
        _dbContext.Lessons.Update(lesson);
        await _dbContext.SaveChangesAsync();
        return lesson;
    }

    public async Task<ICollection<Lesson>?> GetByCourseIdAsync(Guid courseId)
    {
        return  await _dbContext.Lessons.Where(l => l.CourseId == courseId).ToListAsync();
    }
    
}