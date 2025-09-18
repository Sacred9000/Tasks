using System.ComponentModel.DataAnnotations;
namespace MyApp.Api.Models
{
    public class PermissionsGroup
    {
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public int PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;

        public DateTime GrantedAt { get; set; } = DateTime.UtcNow;
    }
}