using System.ComponentModel.DataAnnotations.Schema;
using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Models.Entities.Assignments;

public class PointsEntry : BaseEntity
{
    public Ulid StudentId { get; set; }
    public Ulid TeacherId { get; set; }

    public int Points { get; set; }

    public Ulid? ReasonId { get; set; }

    public Ulid? AssignmentId { get; set; }

    public Ulid? ExamId { get; set; }

    public string? Description { get; set; }

    [ForeignKey(nameof(StudentId))]
    public ApplicationUser? Student { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public ApplicationUser? Teacher { get; set; }

    [ForeignKey(nameof(ReasonId))]
    public Reason? Reason { get; set; }

    [ForeignKey(nameof(AssignmentId))]
    public Assignment? Assignment { get; set; }

    [ForeignKey(nameof(ExamId))]
    public Exam? Exam { get; set; }
}
