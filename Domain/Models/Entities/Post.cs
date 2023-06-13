using Domain.Models.Common;

namespace Domain.Models.Entities;

public class Post:BaseAuditableEntity
{
    public string Content { get; set; }
    public string? Caption { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }
}
