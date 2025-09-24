namespace MyApp.Web.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        // Optional: if you ever fetch groups along with users
        public List<User> Users { get; set; } = new();
    }
    namespace MyApp.Web.Models
{
    public class GroupUserCount
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public int UserCount { get; set; }
    }

    public class GroupIndexViewModel
    {
        public IEnumerable<Group> Groups { get; set; } = new List<Group>();
        public int TotalGroupCount { get; set; }
        public IEnumerable<GroupUserCount> UsersCountPerGroup { get; set; } = new List<GroupUserCount>();
    }
}

}