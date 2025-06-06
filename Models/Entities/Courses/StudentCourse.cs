using System.ComponentModel.DataAnnotations.Schema;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Models.Entities.Courses;

public class StudentCourse
{
    // Composite Key
    public Ulid StudentId { get; set; }

    // Composite Key
    public Ulid CourseId { get; set; }

    public Ulid PrimaryTeacherId { get; set; }

    public DateTime EnrollmentDate { get; set; }

    [ForeignKey(nameof(StudentId))]
    public ApplicationUser? Student { get; set; }

    [ForeignKey(nameof(CourseId))]
    public Course? Course { get; set; }

    [ForeignKey(nameof(PrimaryTeacherId))]
    public ApplicationUser? PrimaryTeacher { get; set; }
    public ICollection<Group>? Groups { get; set; } // each student has many group but with only with one course one group
}
