
using System.ComponentModel.DataAnnotations;
namespace MyApp.Api.Models
{

    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string Usersurname { get; set; } = null!;
        [MaxLength(100)]
        public string Email { get; set; } = null!;
    
    public ICollection<UserGroup> UserGroup { get; set; } = new List<UserGroup>();

    }


}