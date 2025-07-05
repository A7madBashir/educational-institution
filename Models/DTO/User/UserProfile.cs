namespace EducationalInstitution.Models.DTO.User;

public class UserProfile
{
    public Ulid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Name { get; set; }
    public string? UserName { get; set; }
    public DateTime? LastLoginAt { get; set; }
}
