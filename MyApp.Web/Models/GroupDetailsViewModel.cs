namespace MyApp.Web.Models
{
    public class GroupDetailsViewModel
    {
        public Group Group { get; set; } = new();
        public List<User> Members { get; set; } = new();

        public List<User> AvailableUsers { get; set; } = new();
    }
}
