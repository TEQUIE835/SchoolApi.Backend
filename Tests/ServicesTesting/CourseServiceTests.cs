using _1.Application.Interfaces.CourseInterfaces;
using _1.Application.Interfaces.LessonInterfaces;
using _1.Application.Services;
using _2.Domain.Entities;

namespace Tests.ServicesTesting;

public class CourseServiceTests
{
    private readonly Mock<ICourseRepository> _courseRepoMock = new();
    private readonly Mock<ILessonRepository> _lessonRepoMock = new();

    private readonly CourseService _service;

    public CourseServiceTests()
    {
        _service = new CourseService(
            _courseRepoMock.Object,
            _lessonRepoMock.Object
        );
    }
    [Fact]
    public async Task PublishCourse_WithLessons_ShouldSucceed()
    {
        var courseId = Guid.NewGuid();

        var course = new Course
        {
            Id = courseId,
            Status = CourseStatus.Draft,
            Lessons =
            {
                new Lesson { IsDeleted = false }
            }
        };

        _courseRepoMock
            .Setup(r => r.GetCourseByIdWithIncludesAsync(courseId))
            .ReturnsAsync(course);

        var result = await _service.PublishCourseAsync(courseId);

        Assert.NotNull(result);
        Assert.Equal(CourseStatus.Published.ToString(), result!.Status);

        _courseRepoMock.Verify(r => r.UpdateCourse(course), Times.Once);
    }
        
    [Fact]
    public async Task PublishCourse_WithoutLessons_ShouldFail()
    {
        var courseId = Guid.NewGuid();

        var course = new Course
        {
            Id = courseId,
            Lessons = new List<Lesson>()
        };

        _courseRepoMock
            .Setup(r => r.GetCourseByIdWithIncludesAsync(courseId))
            .ReturnsAsync(course);

        await Assert.ThrowsAsync<ArgumentException>(() =>
            _service.PublishCourseAsync(courseId));
    }
        
    [Fact]
    public async Task DeleteCourse_ShouldBeSoftDelete()
    {
        var courseId = Guid.NewGuid();

        var lessons = new List<Lesson>
        {
            new Lesson { IsDeleted = false },
            new Lesson { IsDeleted = false }
        };

        var course = new Course
        {
            Id = courseId,
            IsDeleted = false,
            Lessons = lessons
        };

        _courseRepoMock
            .Setup(r => r.GetCourseByIdWithIncludesAsync(courseId))
            .ReturnsAsync(course);

        var result = await _service.DeleteCourseByIdAsync(courseId);

        Assert.True(course.IsDeleted);
        Assert.All(course.Lessons, l => Assert.True(l.IsDeleted));

        _courseRepoMock.Verify(r => r.UpdateCourse(course), Times.Once);
        _lessonRepoMock.Verify(r => r.UpdateLesson(It.IsAny<Lesson>()), Times.Exactly(2));
    }



}