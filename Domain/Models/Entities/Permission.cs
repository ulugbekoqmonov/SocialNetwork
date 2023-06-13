using Domain.Models.Common;

namespace Domain.Models.Entities;

public class Permission:BaseAuditableEntity
{    
    public string PermissionName { get; set; }
    public virtual ICollection<Role> Roles { get; set; }
}