using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Entities.Activities;
using EducationalInstitution.Models.Entities.Assignments;
using EducationalInstitution.Models.Entities.Blogs;
using EducationalInstitution.Models.Entities.Courses;
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
    public string? Gender { get; set; }
    public string? Nationality { get; set; }
    public string? Job { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DateOfBirth { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }

    public ICollection<Course>? TeacherCourses { get; set; } // related to assistant teachers
    public ICollection<Course>? AssistantTeachersCourses { get; set; } // related to assistant teachers
    public ICollection<StudentCourse>? StudentCourses { get; set; }
    public ICollection<Report>? GeneratedReports { get; set; }
    public ICollection<Message>? SentMessages { get; set; }
    public ICollection<Message>? ReceivedMessages { get; set; }
    public ICollection<Request>? SubmittedRequests { get; set; }
    public ICollection<Request>? RepliedRequests { get; set; }
    public ICollection<Attendance>? Attendances { get; set; }
    public ICollection<Compliment>? GivenCompliments { get; set; }
    public ICollection<Compliment>? ReceivedCompliments { get; set; }
    public ICollection<Note>? WrittenNotes { get; set; }
    public ICollection<Notification>? ReceivedNotifications { get; set; }
    public ICollection<BlogPost>? AuthoredBlogPosts { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<Event>? AttendedEvents { get; set; }
    public ICollection<WorkTimeLog>? WorkTimeLogs { get; set; }
    public ICollection<ActivityLog>? ActivityLogs { get; set; }
    public ICollection<PointsEntry>? AssignedPointsEntries { get; set; }
    public ICollection<PointsEntry>? EarnedPointsEntries { get; set; }
}
