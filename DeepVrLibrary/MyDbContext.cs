using DeepVrLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DeepVrLibrary;

public class MyDbContext : DbContext
{
    
    public MyDbContext(DbContextOptions<MyDbContext> options):  base(options){}

    public DbSet<User> Users { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id).HasName("PRIMARY");
            e.ToTable("Users");
            
            e.Property(u => u.Id).HasColumnName("id");
            e.Property(u => u.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            e.Property(u => u.Password).HasColumnName("password").IsRequired();

            e.Property(u => u.RefreshToken).HasColumnName("refresh_token");

            e.Property(u => u.Role).HasColumnName("role").HasConversion<string>();
        });
    }
    
}