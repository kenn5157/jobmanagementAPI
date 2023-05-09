using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.UserID)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

    }

   
    public DbSet<User> UserTable { get; set; }
}