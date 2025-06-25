using Microsoft.AspNetCore.Identity;

namespace EducationalInstitution.Models.DTO.Responses.Identity;

public class LoginResponse
{
    public bool Succeeded { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime TokenValidTo { get; set; }
    public DateTime RefreshTokenValidTo { get; set; }
    public IEnumerable<IdentityError>? Errors { get; set; }
}
