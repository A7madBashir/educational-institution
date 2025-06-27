using EducationalInstitution.Models.DTO.User;

namespace EducationalInstitution.Models.DTO.Responses.Courses;

public class CoursesResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public UserProfile PrimaryTeacher { get; set; }
}
