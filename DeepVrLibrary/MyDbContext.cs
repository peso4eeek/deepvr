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
            e.ToTable("users");
            
            e.Property(u => u.Id).HasColumnName("id");
            e.Property(u => u.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            e.Property(u => u.Password).HasColumnName("password").IsRequired();

            e.Property(u => u.RefreshToken).HasColumnName("refresh_token");

            e.Property(u => u.Role).HasColumnName("role").HasConversion<string>();
        });

        modelBuilder.Entity<Metrics>(e =>
        {
            e.HasKey(m => m.Id).HasName("PRIMARY");
            e.ToTable("metrics");
            
            e.Property(m => m.Id).HasColumnName("id");
            e.Property(m => m.Uuid).HasColumnName("uuid").IsRequired();
            e.Property(m => m.Ip).HasColumnName("ip").IsRequired();
            e.Property(m => m.Cpu).HasColumnName("cpu");
            e.Property(m => m.Ram).HasColumnName("ram");
            e.Property(m => m.ReceivedAt).HasColumnName("received_at");
        });
        
    }
    
}