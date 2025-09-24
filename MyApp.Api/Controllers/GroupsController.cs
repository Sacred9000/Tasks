using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Api.Data;
using MyApp.Api.Models;

namespace MyApp.Api.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GroupController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/group
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        // GET: api/groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);

            if (group == null) return NotFound();

            return group;
        }

        // GET: api/groups/count
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetGroupCount()
        {
            return await _context.Groups.CountAsync();
        }

        // POST: api/groups
        [HttpPost]
        public async Task<ActionResult<Group>> CreateGroup(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGroup), new { id = group.Id }, group);
        }

        // PUT: api/groups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup(int id, Group group)
        {
            if (id != group.Id) return BadRequest();

            _context.Entry(group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Groups.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) return NotFound();

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/groups/userscount
        [HttpGet("userscount")]
        public async Task<ActionResult<IEnumerable<object>>> GetUserCountsPerGroup()
        {
            var result = await _context.Groups
                .Select(g => new
                {
                    GroupId = g.Id,
                    GroupName = g.Name,
                    UserCount = g.UserGroup.Count
                }).
            ToListAsync();

            return Ok(result);
        }
    }
    
}
