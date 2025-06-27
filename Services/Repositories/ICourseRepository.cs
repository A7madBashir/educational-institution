using EducationalInstitution.Data;
using EducationalInstitution.Models.Entities.Courses;

namespace EducationalInstitution.Services.Repositories;

public interface ICourseRepository : IUlidRepository<Course> { }

public class CourseRepository(ApplicationDbContext db)
    : UlidRepository<Course>(db),
        ICourseRepository { }
