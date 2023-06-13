using Domain.Models.Common;

namespace Domain.Models.Entities;

public class Role:BaseAuditableEntity
{
    public string RoleName { get; set; }
    public virtual ICollection<User> Users { get; set; }
    public virtual ICollection<Permission> Permissions { get; set; }
}