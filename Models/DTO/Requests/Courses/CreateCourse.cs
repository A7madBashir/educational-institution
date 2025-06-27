namespace EducationalInstitution.Models.DTO.Requests.Courses;

public class CreateCourse : ICreateRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string PrimaryTeacherId { get; set; }
}
