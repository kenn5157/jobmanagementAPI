using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Problem>(entity =>
            {
                entity.HasKey(e => e.ProblemId);
                entity.Property(e => e.ProblemName).IsRequired();
                entity.Property(e => e.Location).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Description).IsRequired();
            });
    }

    public DbSet<Problem> ProblemTable { get; set; }
}