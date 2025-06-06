using System.ComponentModel.DataAnnotations.Schema;
using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Entities.Activities;
using EducationalInstitution.Models.Entities.Assignments;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Models.Entities.Courses;

public class Course : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    
    public Ulid PrimaryTeacherId { get; set; }
    [ForeignKey(nameof(PrimaryTeacherId))]
    public ApplicationUser? PrimaryTeacher { get; set; }

    public ICollection<ApplicationUser>? AssistantTeachers { get; set; }
    public ICollection<Assignment>? Assignments { get; set; }
    public ICollection<Attendance>? Attendances { get; set; }
    public ICollection<Group>? Groups { get; set; }
    public ICollection<StudentCourse>? StudentCourses { get; set; }
    public ICollection<Exam>? Exams { get; set; }
    public ICollection<Report>? Reports { get; set; }
}
