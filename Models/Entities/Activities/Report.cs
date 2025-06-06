using System.ComponentModel.DataAnnotations.Schema;
using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Entities.Courses;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Models.Entities.Activities;

public class Report : BaseEntity
{
    public Ulid StudentId { get; set; }

    public Ulid TeacherId { get; set; }

    public Ulid CourseId { get; set; }

    public string? Comments { get; set; }

    [ForeignKey(nameof(StudentId))]
    public ApplicationUser? Student { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public ApplicationUser? Teacher { get; set; }

    [ForeignKey(nameof(CourseId))]
    public Course? Course { get; set; }
}
