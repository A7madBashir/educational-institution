using System.ComponentModel.DataAnnotations.Schema;
using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Models.Entities.Blogs;

public class BlogPost : BaseEntity
{
    public Ulid AuthorId { get; set; }

    public required string Title { get; set; }

    public required string Content { get; set; }

    public required string Type { get; set; }

    [ForeignKey(nameof(AuthorId))]
    public ApplicationUser? Author { get; set; }

    public ICollection<Comment>? Comments { get; set; }
}
