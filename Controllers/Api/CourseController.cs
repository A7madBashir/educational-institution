using System.Diagnostics;
using EducationalInstitution.Mapper;
using EducationalInstitution.Models.DTO.Requests.Courses;
using EducationalInstitution.Models.DTO.Responses.Courses;
using EducationalInstitution.Models.Entities.Courses;
using EducationalInstitution.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace EducationalInstitution.Controllers.Api;

[Route("[controller]")]
public class CourseController(ICourseRepository repository, AppMapper mapper)
    : CrudController<Course, Ulid, CoursesResponse, CreateCourse, EditCourse>(repository, mapper)
{
    protected override string[] GetSearchableProperties()
    {
        return [];
    }

    protected override string[] IncludeNavigation()
    {
        return [nameof(Course.PrimaryTeacher)];
    }

    protected override async Task<OneOf<Success, Error<string>>> BeforeCreateAsync(CreateCourse createDto)
    {
        //TODO: check user if exist
        return new Success();
    }
}
