using EducationalInstitution.Models.Common.BaseEntity;
using Microsoft.AspNetCore.Routing.Constraints;

namespace EducationalInstitution.Models.Entities.Assignments;

public class Reason : BaseEntity
{
    public required string Name { get; set; }

    public required string Description { get; set; }
}
