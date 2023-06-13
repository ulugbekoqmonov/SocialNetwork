using Domain.Models.Common;

namespace Domain.Models.Entities;

public class User : BaseAuditableEntity
{
    public string Fullname { get; set; }
    
    public string UserName { get; set; }

    public string Password { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }
    public virtual ICollection<Role> Roles { get; set; }
    public virtual ICollection<Post> Posts { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
}