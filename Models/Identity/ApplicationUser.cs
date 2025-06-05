using EducationalInstitution.Models.Common.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace EducationalInstitution.Models.Identity;

public class ApplicationUser : IdentityUser<Ulid>, IEntity<Ulid>
{
    public ApplicationUser()
    {
        Id = Ulid.NewUlid();
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public string? Gender { get; set; }
    public string? Nationality { get; set; }
    public string? Job { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public DateTime CreatedAt { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? LastModifiedAt { get; set; }
}
