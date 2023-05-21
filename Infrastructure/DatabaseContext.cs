using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.userID)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.email)
            .IsUnique();
            
        modelBuilder.Entity<Problem>(entity =>
            {
                entity.HasKey(e => e.ProblemId);
                entity.Property(e => e.ProblemName).IsRequired();
                entity.Property(e => e.Longitude).IsRequired();
                entity.Property(e => e.Latitude).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Description).IsRequired();
                entity.Property(e => e.Image).IsRequired();
            });
    }

    public DbSet<User> UserTable { get; set; }
    public DbSet<Problem> ProblemTable { get; set; }
}