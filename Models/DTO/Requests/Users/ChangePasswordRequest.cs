using System.ComponentModel.DataAnnotations;

namespace EducationalInstitution.Models.DTO.Requests.Users;

public class ChangePasswordRequest
{
    public required string Id { get; set; }
    public required string Password { get; set; }

    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}
