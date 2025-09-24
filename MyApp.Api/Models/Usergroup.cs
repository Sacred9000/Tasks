using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace MyApp.Api.Models
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int GroupId { get; set; }

        [JsonIgnore]
        public Group Group { get; set; } = null!;

    }
}