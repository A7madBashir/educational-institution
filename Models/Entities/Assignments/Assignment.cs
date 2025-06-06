using System.ComponentModel.DataAnnotations.Schema;
using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Entities.Courses;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Models.Entities.Assignments;

public class Assignment : BaseEntity
{
    public Ulid CourseId { get; set; }

    public required Ulid TeacherId { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public DateTime DueDate { get; set; }

    [ForeignKey(nameof(CourseId))]
    public Course? Course { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public ApplicationUser? Teacher { get; set; }
}
