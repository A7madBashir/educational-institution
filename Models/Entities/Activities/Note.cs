using System.ComponentModel.DataAnnotations.Schema;
using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Models.Entities.Activities;

public class Note : BaseEntity
{
    public Ulid StudentId { get; set; }

    public Ulid TeacherId { get; set; }

    public required string Content { get; set; }

    [ForeignKey(nameof(StudentId))]
    public ApplicationUser? Student { get; set; }
    
    [ForeignKey(nameof(TeacherId))]
    public ApplicationUser? Teacher { get; set; }
}
