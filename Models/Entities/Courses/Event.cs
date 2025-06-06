using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Models.Entities.Courses;

public class Event : BaseEntity
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string? Location { get; set; }

    public ICollection<ApplicationUser>? Attendees { get; set; }
}
