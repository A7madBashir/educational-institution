namespace EducationalInstitution.Models.DTO.Responses.Identity;

public class RefreshTokenResponse
{
    public bool Succeeded { get; set; }
    public required string RefreshToken { get; set; }
    public DateTime RefreshTokenValidTo { get; set; }
}
