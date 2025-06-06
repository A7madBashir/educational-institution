using System.ComponentModel.DataAnnotations.Schema;
using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Models.Entities.Activities;

public class Notification : BaseEntity
{
    public Ulid UserId { get; set; }
    public required string Message { get; set; }

    public required string Type { get; set; }

    public bool IsRead { get; set; }

    [ForeignKey(nameof(UserId))]
    public ApplicationUser? User { get; set; }
}
