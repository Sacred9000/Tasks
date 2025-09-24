using Microsoft.EntityFrameworkCore;
using MyApp.Api.Models;

namespace MyApp.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = null!;
        public DbSet<Permission> Permissions { get; set; } = null!;
        public DbSet<UserGroup> UserGroups { get; set; } = null!;
        public DbSet<PermissionsGroup> PermissionsGroup { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite keys for join tables
            modelBuilder.Entity<UserGroup>()
                .HasKey(ug => new { ug.UserId, ug.GroupId });

            modelBuilder.Entity<PermissionsGroup>()
                .HasKey(gp => new { gp.GroupId, gp.PermissionId });

            // User ↔ UserGroup
            modelBuilder.Entity<UserGroup>()
            .HasOne(ug => ug.User)
            .WithMany(u => u.UserGroup)
            .HasForeignKey(ug => ug.UserId);

            // Group ↔ UserGroup
            modelBuilder.Entity<UserGroup>()
           .HasOne(ug => ug.Group)
           .WithMany(g => g.UserGroups)
           .HasForeignKey(ug => ug.GroupId);
            
            base.OnModelCreating(modelBuilder);
        }

            
        }
    

    }
