using FollowingErrors.Entities;
using Microsoft.EntityFrameworkCore;

public class BugsManager : DbContext
{
    public BugsManager(DbContextOptions<BugsManager> options)
        : base(options) { }

    public DbSet<Bug> Bug { get; set; } = default!;

    public DbSet<User> User { get; set; } = default!;

    public DbSet<Project> Project { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}
