using EchoesOfTime.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoesOfTime.Database;

public class TimeTrackerDbContext : DbContext
{
    public DbSet<TaskModel> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure SQLite database
        optionsBuilder.UseSqlite("Data Source=TimeTracker.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the TaskModel table
        modelBuilder.Entity<TaskModel>(entity =>
        {
            entity.HasKey(t => t.Id); // Primary key
            entity.Property(t => t.Name).IsRequired(false);
            entity.Property(t => t.Date).IsRequired();
            entity.Property(t => t.ElapsedTime).IsRequired();
            entity.Property(t => t.ActiveSince).IsRequired(false);
            entity.Property(t => t.Details).HasMaxLength(500);
        });
    }
}
