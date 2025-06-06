using System.ComponentModel.DataAnnotations.Schema;
using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Models.Entities.Activities;

public class Request : BaseEntity
{
    public Ulid SenderId { get; set; }

    public Ulid? ReplierId { get; set; }
    public required string Content { get; set; }

    public required string Status { get; set; }

    public DateTime? RepliedAt { get; set; }

    [ForeignKey(nameof(SenderId))]
    public ApplicationUser? Sender { get; set; }

    [ForeignKey(nameof(ReplierId))]
    public ApplicationUser? Replier { get; set; }
}
