using Domain.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models.Entities;

public class UserRefreshToken:BaseEntity
{    
    [Required]
    public string UserName { get; set; }
  
    [Required]
    public string RefreshToken { get; set; }
    
    public DateTime? ExpiredTime { get; set; }
}