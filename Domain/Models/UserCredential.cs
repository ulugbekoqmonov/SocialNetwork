using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models;

public class UserCredential
{
    [JsonPropertyName("user_name")]
    public string UserName { get; set; }
    
    public string Password { get; set; }
}