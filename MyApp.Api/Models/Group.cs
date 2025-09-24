using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace MyApp.Api.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; } =null!;

        public string Description { get; set; } = null!;
        public ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
        // Navigation property example
        [JsonIgnore]
        public ICollection<UserGroup> UserGroup { get; set; } = new List<UserGroup>();
        [JsonIgnore]
        public ICollection<PermissionsGroup> PermissionsGroup { get; set; } = new List<PermissionsGroup>();
    }
}