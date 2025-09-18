using System.ComponentModel.DataAnnotations;
namespace MyApp.Api.Models
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}