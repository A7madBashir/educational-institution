using System.Text.Json.Serialization;

namespace EducationalInstitution.Models.DTO.Requests.Courses;

public class EditCourse : IEditRequest<Ulid>
{
    [JsonIgnore]
    public Ulid Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; }
    public required string PrimaryTeacherId { get; set; }
}
