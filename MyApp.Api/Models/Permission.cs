using System.ComponentModel.DataAnnotations;
namespace MyApp.Api.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

    }
}