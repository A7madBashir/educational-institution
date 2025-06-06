using System.ComponentModel.DataAnnotations.Schema;
using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Models.Entities.Courses;

public class Attendance : BaseEntity
{
    public Ulid StudentId { get; set; }

    public Ulid CourseId { get; set; }

    public Ulid TeacherId { get; set; }

    public required string Status { get; set; }

    public required DateTime Date { get; set; }

    [ForeignKey(nameof(StudentId))]
    public ApplicationUser? Student { get; set; }

    [ForeignKey(nameof(CourseId))]
    public Course? Course { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public ApplicationUser? Teacher { get; set; }
}
