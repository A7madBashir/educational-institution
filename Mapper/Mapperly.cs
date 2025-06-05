using EducationalInstitution.Models.DTO.User;
using EducationalInstitution.Models.Identity;
using Riok.Mapperly.Abstractions;

namespace EducationalInstitution.Mapper;

[Mapper(AllowNullPropertyAssignment = false)]
public partial class AppMapper
{
    [MapPropertyFromSource(nameof(UserProfile.Name), Use = nameof(CombineNames))]
    public partial UserProfile UserResponse(ApplicationUser user);

    [UserMapping]
    private string CombineNames(ApplicationUser user)
    {
        string name = $"{user.FirstName} {user.LastName}";
        return (string.IsNullOrEmpty(name) ? user.UserName : name) ?? "";
    }
}
