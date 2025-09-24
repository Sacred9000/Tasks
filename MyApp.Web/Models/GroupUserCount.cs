namespace MyApp.Web.Models
{
    public class GroupUserCount
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public int UserCount { get; set; }
    }
}