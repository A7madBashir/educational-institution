using System.Text.Json.Serialization;

namespace EducationalInstitution.Models.DTO.Requests;

public interface IEditRequest<T>
    where T : IEquatable<T>
{
    [JsonIgnore]
    T Id { get; set; }
}
