using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Api.Data;
using MyApp.Api.Models;

namespace MyApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PermissionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/permissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Permission>>> GetPermissions()
        {
            return await _context.Permissions.ToListAsync();
        }

        // GET: api/permissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Permission>> GetPermission(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null) return NotFound();

            return permission;
        }

        // GET: api/permissions/count
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetPermissionCount()
        {
            return await _context.Permissions.CountAsync();
        }

        // POST: api/permissions
        [HttpPost]
        public async Task<ActionResult<Permission>> CreatePermission(Permission permission)
        {
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPermission), new { id = permission.Id }, permission);
        }

        // PUT: api/permissions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePermission(int id, Permission permission)
        {
            if (id != permission.Id) return BadRequest();

            _context.Entry(permission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Permissions.Any(e => e.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // DELETE: api/permissions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null) return NotFound();

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
