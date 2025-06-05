namespace EducationalInstitution.Models.DTO.Requests.Users;

public class RegisterUser
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}
