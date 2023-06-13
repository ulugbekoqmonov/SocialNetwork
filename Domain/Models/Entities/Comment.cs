using Domain.Models.Common;

namespace Domain.Models.Entities;

public class Comment:BaseAuditableEntity
{
    public string Text { get; set; }

    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    public Guid? ReplyCommentId { get; set; }
    public virtual Comment? ReplyComment { get; set; }

    public Guid PostId { get; set; }
    public virtual Post Post { get; set; }
    public virtual ICollection<Comment> ReplyComments { get; set;}
}
