using System.ComponentModel.DataAnnotations.Schema;
using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Models.Entities.Activities;

public class Message : BaseEntity
{
    public Ulid SenderId { get; set; }

    public Ulid ReceiverId { get; set; }

    public required string Content { get; set; }

    public DateTime SentAt { get; set; }

    [ForeignKey(nameof(SenderId))]
    public ApplicationUser? Sender { get; set; }

    [ForeignKey(nameof(ReceiverId))]
    public ApplicationUser? Receiver { get; set; }
}
