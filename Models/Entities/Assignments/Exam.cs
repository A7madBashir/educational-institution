using System.ComponentModel.DataAnnotations.Schema;
using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Entities.Courses;

namespace EducationalInstitution.Models.Entities.Assignments;

public class Exam : BaseEntity
{
    public Ulid CourseId { get; set; }

    public required string Title { get; set; }

    public required DateTime Date { get; set; }

    public required int MaxScore { get; set; }

    [ForeignKey(nameof(CourseId))]
    public Course? Course { get; set; }
}
