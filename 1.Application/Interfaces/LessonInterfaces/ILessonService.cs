using _1.Application.DTOs.LessonDtos;
using _2.Domain.Entities;

namespace _1.Application.Interfaces.LessonInterfaces;

public interface ILessonService
{
    public Task<LessonShowDto?> GetLessonByIdAsync(Guid lessonId);
    public Task<ICollection<LessonShowDto>?> GetAllLessonsAsync();
    public Task<LessonShowDto?> AddLesson(LessonCreateDto lesson);
    public Task<LessonShowDto?> UpdateLesson(LessonCreateDto lesson);
    public Task<ICollection<LessonShowDto>?> GetByCourseIdAsync(Guid courseId);
    public  Task<LessonShowDto?> DeleteLessonByIdAsync(Guid lessonId);
}