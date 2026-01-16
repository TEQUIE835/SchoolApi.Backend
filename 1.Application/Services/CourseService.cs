using _1.Application.DTOs.CourseDtos;
using _1.Application.Interfaces.CourseInterfaces;
using _1.Application.Interfaces.LessonInterfaces;
using _2.Domain.Entities;

namespace _1.Application.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILessonRepository _lessonRepository;

    public CourseService(ICourseRepository courseRepository, ILessonRepository lessonRepository)
    {
        _courseRepository = courseRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<CourseShowDto?> GetCourseByIdAsync(Guid courseId)
    {
        var course = await _courseRepository.GetCourseByIdAsync(courseId);
        if (course == null) throw new ArgumentException($"Course with id {courseId} does not exist");
        return new CourseShowDto(){Id = course.Id,CreatedAt =  course.CreatedAt,IsDeleted = course.IsDeleted,Status = course.Status.ToString(),Title = course.Title,UpdatedAt = course.UpdatedAt};
    }

    public async Task<ICollection<CourseShowDto>?> GetAllCoursesAsync()
    {
        var courses = await _courseRepository.GetAllCoursesAsync();
        var showCourses = new List<CourseShowDto>();
        foreach (var course in courses)
        {
            showCourses.Add(new CourseShowDto(){Id = course.Id,CreatedAt =  course.CreatedAt,IsDeleted = course.IsDeleted,Status = course.Status.ToString(),Title = course.Title,UpdatedAt = course.UpdatedAt});
        }
        return showCourses;
    }

    public async Task<CourseShowDto?> AddCourse(CourseCreateDto course)
    {
        var newCourse = new Course() { Title = course.Title };
        newCourse = await _courseRepository.AddCourse(newCourse);
        return new CourseShowDto(){CreatedAt = newCourse.CreatedAt, Id=newCourse.Id, IsDeleted = newCourse.IsDeleted, Status = newCourse.Status.ToString(), Title = newCourse.Title, UpdatedAt = newCourse.UpdatedAt};
    }

    public async Task<CourseShowDto?> UpdateCourse(CourseCreateDto course)
    {
        var storedCourse = await _courseRepository.GetCourseByIdAsync(course.CourseId);
        if (storedCourse == null) throw new ArgumentException($"Course with id {course.CourseId} does not exist");
        storedCourse.Title = course.Title;
        storedCourse.UpdatedAt = DateTime.UtcNow;
        await _courseRepository.UpdateCourse(storedCourse);
        return new CourseShowDto()
        {
            Id = storedCourse.Id, CreatedAt = storedCourse.CreatedAt, IsDeleted = storedCourse.IsDeleted,
            Status = storedCourse.Status.ToString(), Title = storedCourse.Title, UpdatedAt = storedCourse.UpdatedAt
        };
    }

    public async Task<CourseShowDto?> DeleteCourseByIdAsync(Guid courseId)
    {
        var course = await _courseRepository.GetCourseByIdWithIncludesAsync(courseId);
        if (course == null) throw new ArgumentException($"Course with id {courseId} does not exist");
        foreach (var lesson in course.Lessons)
        {
            if (!lesson.IsDeleted)
            {
                lesson.IsDeleted = true;
                lesson.UpdatedAt = DateTime.UtcNow;
                await _lessonRepository.UpdateLesson(lesson);
            }
        }
        course.IsDeleted = true;
        await _courseRepository.UpdateCourse(course);
        return new CourseShowDto(){Id = course.Id,CreatedAt =  course.CreatedAt,IsDeleted = course.IsDeleted,Status = course.Status.ToString(),Title = course.Title,UpdatedAt = course.UpdatedAt};
    }

    public async Task<CourseShowDto?> PublishCourseAsync(Guid courseId)
    {
        var course = await _courseRepository.GetCourseByIdWithIncludesAsync(courseId);
        if (course == null) throw new ArgumentException($"Course with id {courseId} does not exist");
        if (course.Lessons.Count < 1)
            throw new ArgumentException("The course requires at least 1 active lesson to publish");
        var deletedLessons = new List<Lesson>();
        foreach (var lesson in course.Lessons)
        {
            if (lesson.IsDeleted) 
                deletedLessons.Add(lesson);
        }
        if (deletedLessons.Count >= course.Lessons.Count) throw new ArgumentException("The course requires at least 1 active lesson to publish");
        course.Status = CourseStatus.Published;
        await _courseRepository.UpdateCourse(course);
        return new CourseShowDto(){Id = course.Id,CreatedAt =  course.CreatedAt,IsDeleted = course.IsDeleted,Status = course.Status.ToString(),Title = course.Title,UpdatedAt = course.UpdatedAt};
    }

    public async Task<CourseShowDto?> GetCourseWithLessonsByCourseIdAsync(Guid courseId)
    {
        var course = await _courseRepository.GetCourseByIdWithIncludesAsync(courseId);
        if(course == null) throw new ArgumentException($"Course with id {courseId} does not exist");
        return new CourseShowDto(){Id = course.Id,CreatedAt =  course.CreatedAt,IsDeleted = course.IsDeleted,Status = course.Status.ToString(),Title = course.Title,UpdatedAt = course.UpdatedAt};
    }

    public async Task<CourseSummaryDto?> GetCourseSummaryByCourseIdAsync(Guid courseId)
    {
        var course = await _courseRepository.GetCourseByIdWithIncludesAsync(courseId);
        if (course == null) throw new ArgumentException($"Course with id {courseId} does not exist");
        return new CourseSummaryDto()
        {
            CourseId = course.Id,
            CourseTitle = course.Title,
            LessonsCount = course.Lessons.Count,
            UpdatedAt = course.UpdatedAt
        };
    }

    public async Task<CourseShowDto?> UnPublishCourseAsync(Guid courseId)
    {
        var course = await _courseRepository.GetCourseByIdAsync(courseId);
        if (course == null) throw new ArgumentException($"Course with id{courseId} does not exist");
        course.Status = CourseStatus.Draft;
        course = await _courseRepository.UpdateCourse(course);
        return new CourseShowDto(){Id = course.Id,CreatedAt =  course.CreatedAt,IsDeleted = course.IsDeleted,Status = course.Status.ToString(),Title = course.Title,UpdatedAt = course.UpdatedAt};
    }
}