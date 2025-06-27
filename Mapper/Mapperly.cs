using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.DTO.Requests;
using EducationalInstitution.Models.DTO.Requests.Courses;
using EducationalInstitution.Models.DTO.Responses.Courses;
using EducationalInstitution.Models.DTO.User;
using EducationalInstitution.Models.Entities.Courses;
using EducationalInstitution.Models.Identity;
using Riok.Mapperly.Abstractions;

namespace EducationalInstitution.Mapper;

[Mapper(AllowNullPropertyAssignment = false)]
public partial class AppMapper
{
    // generic mapping
    public partial TTarget ToResponse<TTarget, T>(IEntity<T> source)
        where T : IEquatable<T>;

    public partial TTarget ToEditModel<TTarget, T>(IEntity<T> source)
        where T : IEquatable<T>;

    public partial TEntity ToEntity<TEntity, T>(ICreateRequest source)
        where TEntity : class, IEntity<T>
        where T : IEquatable<T>;

    public partial TEntity ToEntity<TEntity, T>(IEditRequest<T> source)
        where TEntity : class, IEntity<T>
        where T : IEquatable<T>;

    [MapPropertyFromSource(nameof(UserProfile.Name), Use = nameof(CombineNames))]
    public partial UserProfile ToUserProfile(ApplicationUser user);

    [UserMapping]
    private string CombineNames(ApplicationUser user)
    {
        string name = $"{user.FirstName} {user.LastName}";
        return (string.IsNullOrEmpty(name) ? user.UserName : name) ?? "";
    }

    // Course
    public partial Course ToEntity(CreateCourse source);

    public partial Course ToEntity(EditCourse source);

    public partial CoursesResponse ToResponse(Course source);
}
