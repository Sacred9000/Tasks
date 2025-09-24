using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Api.Models;
using MyApp.Api.Data;
namespace MyApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserGroupsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserGroupsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/usergroups/add
        [HttpPost("add")]
        public async Task<IActionResult> AddUserToGroup(int userId, int groupId)
        {
            var exists = await _context.UserGroups
                .AnyAsync(ug => ug.UserId == userId && ug.GroupId == groupId);

            if (exists)
                return BadRequest("User already in group.");

            _context.UserGroups.Add(new UserGroup { UserId = userId, GroupId = groupId });
            await _context.SaveChangesAsync();

            return Ok("User added to group.");
        }

        // DELETE: api/usergroups/remove
        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveUserFromGroup(int userId, int groupId)
        {
            var link = await _context.UserGroups
                .FirstOrDefaultAsync(ug => ug.UserId == userId && ug.GroupId == groupId);

            if (link == null)
                return NotFound("User not in group.");

            _context.UserGroups.Remove(link);
            await _context.SaveChangesAsync();

            return Ok("User removed from group.");
        }

        // GET: api/usergroups/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetGroupsForUser(int userId)
        {
            var groups = await _context.UserGroups
                .Where(ug => ug.UserId == userId)
                .Include(ug => ug.User)
                .Select(ug => ug.Group)
                .ToListAsync();

            return Ok(groups);
        }

        // GET: api/usergroups/group/3
        [HttpGet("group/{groupId}")]
        public async Task<IActionResult> GetUsersInGroup(int groupId)
        {
            var users = await _context.UserGroups
                .Where(ug => ug.GroupId == groupId)
                .Select(ug => ug.User)
                .ToListAsync();

            return Ok(users);
        }
        [HttpGet("debug/Usergroups")]
        public async Task<ActionResult> DebugUserGroups()
        {
            var test = await _context.UserGroups
            .Select(ug => new
            {

                ug.UserId,
                ug.GroupId,
            }).ToListAsync();
              
            
            return Ok(test);
            

            
        }
    }
}
