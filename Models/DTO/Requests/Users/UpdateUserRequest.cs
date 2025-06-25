namespace EducationalInstitution.Models.DTO.Requests.Users;

public class UpdateUserRequest : RegisterUser
{
    public required string Id { get; set; }
}
