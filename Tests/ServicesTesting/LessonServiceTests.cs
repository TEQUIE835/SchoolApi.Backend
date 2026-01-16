using _1.Application.DTOs.LessonDtos;
using _1.Application.Interfaces.CourseInterfaces;
using _1.Application.Interfaces.LessonInterfaces;
using _1.Application.Services;
using _2.Domain.Entities;


namespace Tests;

public class LessonServiceTests
{
    private readonly Mock<ICourseRepository> _courseRepoMock = new();
    private readonly Mock<ILessonRepository> _lessonRepoMock = new();

    private readonly LessonService _service;

    public LessonServiceTests()
    {
        _service = new LessonService(
            _lessonRepoMock.Object,
            _courseRepoMock.Object
        );
    }
    
    [Fact]
    public async Task CreateLesson_WithUniqueOrder_ShouldSucceed()
    {
        var courseId = Guid.NewGuid();

        var course = new Course
        {
            Id = courseId,
            Status = CourseStatus.Published,
            IsDeleted = false,
            Lessons =
            {
                new Lesson { Order = 1 }
            }
        };

        var lessonDto = new LessonCreateDto
        {
            CourseId = courseId,
            Title = "Lesson 2",
            Order = 2
        };

        _courseRepoMock
            .Setup(r => r.GetCourseByIdWithIncludesAsync(courseId))
            .ReturnsAsync(course);

        _lessonRepoMock
            .Setup(r => r.AddLesson(It.IsAny<Lesson>()))
            .ReturnsAsync((Lesson l) => l);

        var result = await _service.AddLesson(lessonDto);

        Assert.NotNull(result);
        Assert.Equal(2, result!.Order);
    }
    
    [Fact]
    public async Task CreateLesson_WithDuplicateOrder_ShouldFail()
    {
        var courseId = Guid.NewGuid();

        var course = new Course
        {
            Id = courseId,
            Status = CourseStatus.Published,
            Lessons =
            {
                new Lesson { Order = 1 }
            }
        };

        var lessonDto = new LessonCreateDto
        {
            CourseId = courseId,
            Title = "Duplicate Lesson",
            Order = 1
        };

        _courseRepoMock
            .Setup(r => r.GetCourseByIdWithIncludesAsync(courseId))
            .ReturnsAsync(course);

        await Assert.ThrowsAsync<InvalidDataException>(() =>
            _service.AddLesson(lessonDto));
    }


}