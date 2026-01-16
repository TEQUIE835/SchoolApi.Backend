using _1.Application.DTOs.LessonDtos;
using _1.Application.Interfaces.AuthInterfaces;
using _1.Application.Interfaces.CourseInterfaces;
using _1.Application.Interfaces.LessonInterfaces;
using _2.Domain.Entities;

namespace _1.Application.Services;

public class LessonService :ILessonService
{
    private readonly ILessonRepository _repository;
    private readonly ICourseRepository _courseRepository;
    
    public LessonService(ILessonRepository repository,  ICourseRepository courseRepository)
    {
        _repository = repository;
        _courseRepository = courseRepository;
    }

    public async Task<LessonShowDto?> GetLessonByIdAsync(Guid lessonId)
    {
        var lesson =  await _repository.GetLessonByIdAsync(lessonId);
        if (lesson == null) throw new ArgumentException("lesson not found");
        return new LessonShowDto(){Title = lesson.Title,CourseId = lesson.CourseId,Order = lesson.Order,CreatedAt = lesson.CreatedAt,IsDeleted = lesson.IsDeleted,Id = lesson.Id,UpdatedAt = lesson.UpdatedAt};
    }

    public async Task<ICollection<LessonShowDto>?> GetAllLessonsAsync()
    {
        var lessons =  await _repository.GetAllLessonsAsync();
        var showLessons = new List<LessonShowDto>();
        foreach (var lesson in lessons)
        {
            showLessons.Add(new LessonShowDto()
            {
                Title = lesson.Title, CourseId = lesson.CourseId, Order = lesson.Order, CreatedAt = lesson.CreatedAt,
                IsDeleted = lesson.IsDeleted, Id = lesson.Id, UpdatedAt = lesson.UpdatedAt
            });
        }
        return showLessons;
    }

    public async Task<LessonShowDto?> AddLesson(LessonCreateDto lesson)
    {
        var course = await _courseRepository.GetCourseByIdWithIncludesAsync(lesson.CourseId);
        if (course == null) throw new ArgumentException("Course not found");
        if (course.Status != CourseStatus.Published || course.IsDeleted) throw new ArgumentException("lesson is not published or is deleted");
        foreach (var les in course.Lessons)
        {
            if (lesson.Order == les.Order) throw new InvalidDataException("another lesson has this order");
        }
        
        var newLesson = await _repository.AddLesson(new Lesson{CourseId = lesson.CourseId,Title = lesson.Title, Order = lesson.Order});
        return new LessonShowDto(){Title = newLesson.Title,CourseId = newLesson.CourseId,Order = newLesson.Order,CreatedAt = newLesson.CreatedAt,IsDeleted = newLesson.IsDeleted,Id = newLesson.Id,UpdatedAt = newLesson.UpdatedAt};
    }

    public async Task<LessonShowDto?> UpdateLesson(LessonCreateDto lesson)
    {
        var storedLesson = await _repository.GetLessonByIdAsync(lesson.LessonId);
        if (storedLesson == null) throw new ArgumentException("lesson not found");
        if (storedLesson.IsDeleted)  throw new ArgumentException("lesson is deleted"); 
        var course = await _courseRepository.GetCourseByIdWithIncludesAsync(lesson.CourseId);
        if (course == null) throw new ArgumentException("Course not found");
        if(course.Status != CourseStatus.Published || course.IsDeleted) throw new ArgumentException("lesson is not published or is deleted");
        foreach (var les in course.Lessons)
        {
            if(lesson.Order == les.Order) throw new InvalidDataException("another lesson has this order");
        }
        storedLesson.Order = lesson.Order;
        storedLesson.CourseId = lesson.CourseId;
        storedLesson.Title = lesson.Title;
        storedLesson.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateLesson(storedLesson);
        return new LessonShowDto(){Title = storedLesson.Title,CourseId = storedLesson.CourseId,Order = storedLesson.Order,CreatedAt = storedLesson.CreatedAt,IsDeleted = storedLesson.IsDeleted,Id = storedLesson.Id,UpdatedAt = storedLesson.UpdatedAt};;
    }

    public async Task<ICollection<LessonShowDto>?> GetByCourseIdAsync(Guid courseId)
    {
        var lessons = await _repository.GetByCourseIdAsync(courseId);
        var showLessons = new List<LessonShowDto>();
        foreach (var lesson in lessons)
        {
            showLessons.Add(new LessonShowDto()
            {
                Title = lesson.Title, CourseId = lesson.CourseId, Order = lesson.Order, CreatedAt = lesson.CreatedAt,
                IsDeleted = lesson.IsDeleted, Id = lesson.Id, UpdatedAt = lesson.UpdatedAt
            });
        }
        return showLessons;
    }


    public async Task<LessonShowDto?> DeleteLessonByIdAsync(Guid lessonId)
    {
        var lesson = await _repository.GetLessonByIdAsync(lessonId);
        if (lesson == null) throw new ArgumentException("Lesson not found");
        lesson.IsDeleted = true;
        await _repository.UpdateLesson(lesson);
        return new LessonShowDto()
        {
            Title = lesson.Title, CourseId = lesson.CourseId, Order = lesson.Order, CreatedAt = lesson.CreatedAt,
            IsDeleted = lesson.IsDeleted, Id = lesson.Id, UpdatedAt = lesson.UpdatedAt
        };
    }
}