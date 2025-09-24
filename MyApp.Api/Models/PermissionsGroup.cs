using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace MyApp.Api.Models
{
    public class PermissionsGroup
    {
        public int GroupId { get; set; }
        [JsonIgnore]
        public Group Group { get; set; } = null!;

        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;

        public DateTime? GrantedAt { get; set; } = DateTime.UtcNow;
    }
}