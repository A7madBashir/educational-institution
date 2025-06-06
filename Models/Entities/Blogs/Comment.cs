using System.ComponentModel.DataAnnotations.Schema;
using EducationalInstitution.Models.Common.BaseEntity;
using EducationalInstitution.Models.Identity;

namespace EducationalInstitution.Models.Entities.Blogs;

public class Comment : BaseEntity
{
    public Ulid BlogPostId { get; set; }
    public Ulid UserId { get; set; }

    public required string Content { get; set; }

    [ForeignKey(nameof(BlogPostId))]
    public BlogPost? BlogPost { get; set; }

    [ForeignKey(nameof(UserId))]
    public ApplicationUser? User { get; set; }
}
