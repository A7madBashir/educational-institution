using EducationalInstitution.Models.Common.BaseEntity;
using Microsoft.AspNetCore.Identity;

namespace EducationalInstitution.Models.Identity;

public class ApplicationRole : IdentityRole<Ulid>, IEntity<Ulid>
{
    public ApplicationRole()
    {
        Id = Ulid.NewUlid();
    }
}
