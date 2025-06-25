using EducationalInstitution.Data.Converter;
using EducationalInstitution.Models.Entities.Activities;
using EducationalInstitution.Models.Entities.Assignments;
using EducationalInstitution.Models.Entities.Blogs;
using EducationalInstitution.Models.Entities.Courses;
using EducationalInstitution.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EducationalInstitution.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Ulid>(options)
{
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<Ulid>().HaveConversion<UlidToStringConverter>();
        configurationBuilder.Properties<DateTime>().HaveConversion<DateTimeConverter>();
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Assignment> Assignments { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<PointsEntry> PointsEntries { get; set; }
    public DbSet<Reason> Reasons { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Compliment> Compliments { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<WorkTimeLog> WorkTimeLogs { get; set; }
    public DbSet<ActivityLog> ActivityLogs { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Call the base method first for Identity's model configuration if inheriting IdentityDbContext
        base.OnModelCreating(modelBuilder);

        // --- Configure Many-to-Many Relationships and Join Tables ---

        // 1. StudentCourse (Composite Primary Key)
        modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId }); // Composite PK

        // Define relationships for StudentCourse
        modelBuilder
            .Entity<StudentCourse>()
            .HasOne(sc => sc.Student)
            .WithMany(u => u.StudentCourses)
            .HasForeignKey(sc => sc.StudentId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent student deletion if enrolled

        modelBuilder
            .Entity<StudentCourse>()
            .HasOne(sc => sc.Course)
            .WithMany(c => c.StudentCourses)
            .HasForeignKey(sc => sc.CourseId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent course deletion if students are enrolled

        // Self-referencing FK for PrimaryTeacher in StudentCourse
        modelBuilder
            .Entity<StudentCourse>()
            .HasOne(sc => sc.PrimaryTeacher)
            .WithMany() // No navigation property on User for this specific relationship
            .HasForeignKey(sc => sc.PrimaryTeacherId)
            .OnDelete(DeleteBehavior.Restrict); // Important to avoid cascade delete cycles

        // 2. Course and Teacher (Many-to-Many join between Course and User)
        modelBuilder
            .Entity<ApplicationUser>()
            .HasMany(u => u.AssistantTeachersCourses)
            .WithMany(c => c.AssistantTeachers);

        // 3. Group and Student Course (Many-to-Many join between Group and StudentCourse)
        modelBuilder.Entity<Group>().HasMany(g => g.StudentCourses).WithMany(sc => sc.Groups);

        // 4. UserEvent (Many-to-Many join between User and Event)
        modelBuilder.Entity<Event>().HasMany(u => u.Attendees).WithMany(u => u.AttendedEvents);

        // --- Unique Constraints and Indexes ---
        modelBuilder.Entity<Course>().HasIndex(c => c.Name).IsUnique();

        // Reason: Ensure reason name is unique
        modelBuilder.Entity<Reason>().HasIndex(r => r.Name).IsUnique();

        // --- Additional Relationship Configurations (Important for self-referencing User relationships) ---
        // User has multiple relationships to User. EF Core needs to know which FK maps to which navigation property.

        // User - Message (Sender)
        modelBuilder
            .Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent user deletion if they sent messages

        // User - Message (Receiver)
        modelBuilder
            .Entity<Message>()
            .HasOne(m => m.Receiver)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent user deletion if they received messages

        // User - Request (Sender)
        modelBuilder
            .Entity<Request>()
            .HasOne(req => req.Sender)
            .WithMany(u => u.SubmittedRequests)
            .HasForeignKey(req => req.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        // User - Request (Replier - nullable FK)
        modelBuilder
            .Entity<Request>()
            .HasOne(req => req.Replier)
            .WithMany(u => u.RepliedRequests)
            .HasForeignKey(req => req.ReplierId)
            .IsRequired(false) // Marks FK as nullable
            .OnDelete(DeleteBehavior.SetNull); // Set FK to NULL if replier is deleted

        // User - Compliment (Giver)
        modelBuilder
            .Entity<Compliment>()
            .HasOne(c => c.Giver)
            .WithMany(u => u.GivenCompliments)
            .HasForeignKey(c => c.GiverId)
            .OnDelete(DeleteBehavior.Restrict);

        // User - Compliment (Receiver)
        modelBuilder
            .Entity<Compliment>()
            .HasOne(c => c.Receiver)
            .WithMany(u => u.ReceivedCompliments)
            .HasForeignKey(c => c.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        // User - Note (Student)
        modelBuilder
            .Entity<Note>()
            .HasOne(n => n.Student)
            .WithMany() // No specific collection on User for 'Student' on Note
            .HasForeignKey(n => n.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        // User - Note (Teacher)
        modelBuilder
            .Entity<Note>()
            .HasOne(n => n.Teacher)
            .WithMany(u => u.WrittenNotes)
            .HasForeignKey(n => n.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        // User - PointsEntry (Student)
        modelBuilder
            .Entity<PointsEntry>()
            .HasOne(pe => pe.Student)
            .WithMany(u => u.EarnedPointsEntries)
            .HasForeignKey(pe => pe.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        // User - PointsEntry (Teacher)
        modelBuilder
            .Entity<PointsEntry>()
            .HasOne(pe => pe.Teacher)
            .WithMany(u => u.AssignedPointsEntries)
            .HasForeignKey(pe => pe.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        // --- Fix for Course.PrimaryTeacher relationship ---
        modelBuilder
            .Entity<Course>()
            .HasOne(c => c.PrimaryTeacher)
            .WithMany(u => u.TeacherCourses)
            .HasForeignKey(c => c.PrimaryTeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        // Course - Assignment (Teacher)
        modelBuilder
            .Entity<Assignment>()
            .HasOne(a => a.Teacher)
            .WithMany() // No specific collection on User for 'Teacher' on Assignment
            .HasForeignKey(a => a.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        // User - Report (Teacher)
        modelBuilder
            .Entity<Report>()
            .HasOne(a => a.Teacher)
            .WithMany() // No specific collection on User for 'Teacher' on Assignment
            .HasForeignKey(a => a.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        // User - Report (Student)
        modelBuilder
            .Entity<Report>()
            .HasOne(a => a.Student)
            .WithMany() // No specific collection on User for 'Teacher' on Assignment
            .HasForeignKey(a => a.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Course - Attendance (Teacher)
        modelBuilder
            .Entity<Attendance>()
            .HasOne(a => a.Teacher)
            .WithMany() // No specific collection on User for 'Teacher' on Attendance
            .HasForeignKey(a => a.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
