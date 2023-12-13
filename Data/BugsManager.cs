using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FollowingErrors.Entities;

public class BugsManager : DbContext
{
    public BugsManager(DbContextOptions<BugsManager> options)
        : base(options)
    {
    }

    public DbSet<Bug> Bug { get; set; } = default!;

    public DbSet<User> User { get; set; } = default!;

    public DbSet<Project> Project { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bug>().HasIndex(_ => new { _.ProjectId, _.UserId }).IsUnique();

    }
}