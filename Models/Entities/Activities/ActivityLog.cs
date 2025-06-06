using System.ComponentModel.DataAnnotations.Schema;
using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Models.Entities.Activities;

public class ActivityLog : BaseEntity
{
    public Ulid UserId { get; set; }

    public required string ActivityType { get; set; }

    public string? Description { get; set; }

    public string? RelatedEntity { get; set; } // e.g., "Course", "Assignment"

    public Ulid? RelatedEntityId { get; set; }

    [ForeignKey(nameof(UserId))]
    public ApplicationUser? User { get; set; }
}
