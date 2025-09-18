using System.ComponentModel.DataAnnotations;
namespace MyApp.Api.Models
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } =null!;

        public string Description { get; set; } = null!;   

        // Navigation property example
        public ICollection<UserGroup> UserGroup { get; set; } = new List<UserGroup>();
        public ICollection<PermissionsGroup> PermissionsGroup { get; set; } = new List<PermissionsGroup>();
    }
}