using DeepVrLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DeepVrLibrary;

public class MyDbContext : DbContext
{
    
    public MyDbContext(DbContextOptions<MyDbContext> options):  base(options){}

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Pc> Pcs { get; set; } = null!;
    public DbSet<Metrics> Metrics { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id).HasName("users_pk");
            e.ToTable("users");
            
            e.Property(u => u.Id).HasColumnName("id");
            e.Property(u => u.Name).HasColumnName("name").IsRequired().HasMaxLength(50);
            e.Property(u => u.Password).HasColumnName("password").IsRequired();

            e.Property(u => u.RefreshToken).HasColumnName("refresh_token");

            e.Property(u => u.Role).HasColumnName("role").HasConversion<string>();
        });

        modelBuilder.Entity<Pc>(e =>
        {
            e.HasKey(p => p.Id).HasName("pc_pk");
            e.HasIndex(p => p.Uuid).HasDatabaseName("pc_uuid").IsUnique();
            
            e.ToTable("pc");
            
            e.Property(p => p.Id).HasColumnName("id");
            e.Property(p => p.HostName).HasColumnName("host_name").IsRequired().HasMaxLength(50);
            e.Property(p => p.Uuid).HasColumnName("uuid").IsRequired();
            e.Property(p => p.Ip).HasColumnName("ip").IsRequired().HasMaxLength(50);
            e.Property(p => p.IsBusy).HasColumnName("is_busy");
        });
        
        modelBuilder.Entity<Metrics>(e =>
        {
            e.HasKey(m => m.Id).HasName("metrics_pk");
            e.ToTable("metrics");
            
            e.Property(m => m.Id).HasColumnName("id");
            e.Property(m => m.Uuid).HasColumnName("uuid").IsRequired();
            e.Property(m => m.Ip).HasColumnName("ip").IsRequired();
            e.Property(m => m.Cpu).HasColumnName("cpu").IsRequired();
            e.Property(m => m.Ram).HasColumnName("ram").IsRequired();
            e.Property(m => m.ReceivedAt).HasColumnName("received_at").IsRequired();
        });
    }
    
}