using _2.Domain.Entities;

namespace _1.Application.Interfaces.LessonInterfaces;

public interface ILessonRepository
{
    public Task<Lesson?> GetLessonByIdAsync(Guid lessonId);
    public Task<ICollection<Lesson>?> GetAllLessonsAsync();
    public Task<Lesson?> AddLesson(Lesson lesson);
    public Task<Lesson?> UpdateLesson(Lesson lesson);
    public Task<ICollection<Lesson>?> GetByCourseIdAsync(Guid courseId);
    
}