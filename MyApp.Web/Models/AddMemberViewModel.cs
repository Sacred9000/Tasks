namespace MyApp.Web.Models
{
    public class AddMemberViewModel
    {
        public int GroupId { get; set; }
        public int SelectedUserId { get; set; }
        public List<User> AvailableUsers { get; set; } = new();
    }
}
