using System.ComponentModel.DataAnnotations.Schema;
using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Models.Entities.Courses;

public class Group : BaseEntity
{
    public Ulid CourseId { get; set; }

    public required string Name { get; set; }

    public Ulid TeacherId { get; set; }

    [ForeignKey(nameof(CourseId))]
    public Course? Course { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public ApplicationUser? Teacher { get; set; }

    public ICollection<StudentCourse>? StudentCourses { get; set; }
}
