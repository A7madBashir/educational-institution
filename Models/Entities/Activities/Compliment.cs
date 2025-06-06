using System.ComponentModel.DataAnnotations.Schema;
using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Models.Entities.Activities;

public class Compliment : BaseEntity
{
    public Ulid GiverId { get; set; }
    public Ulid ReceiverId { get; set; }

    public required string Reason { get; set; }

    public int PointsAwarded { get; set; }

    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(GiverId))]
    public ApplicationUser? Giver { get; set; }

    [ForeignKey(nameof(ReceiverId))]
    public ApplicationUser? Receiver { get; set; }
}
