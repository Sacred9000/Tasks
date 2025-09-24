namespace MyApp.Web.Models
{
    public class GroupIndexViewModel
    {
        public IEnumerable<Group> Groups { get; set; } = new List<Group>();
        public int TotalGroupCount { get; set; }
        public IEnumerable<GroupUserCount> UsersCountPerGroup { get; set; } = new List<GroupUserCount>();
    }
}