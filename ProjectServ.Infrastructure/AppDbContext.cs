using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectServ.Domain.Entities;

namespace ProjectServ.Infrastructure;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Application> Applications { get; set; } = null!;
    public DbSet<AppUser> AppUsers { get; set; } = null!;
    public DbSet<AppRole> AppRoles { get; set; } = null!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Application>(builder =>
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.CarNumber).IsRequired().HasMaxLength(256);
            builder.Property(a => a.CarBrand).IsRequired().HasMaxLength(256);
            builder.Property(a => a.TimeOfArrival).IsRequired();
            builder.Property(a => a.ProblemDescription).IsRequired().HasMaxLength(1024);

            builder.HasOne<AppUser>(a => a.User)
                .WithMany(u => u.UserApplications)
                .HasForeignKey(a => a.UserId);
            
            builder.HasOne<AppUser>(a => a.Master)
                .WithMany(u => u.MasterApplications)
                .HasForeignKey(a => a.MasterId);

            builder.Property(a => a.TimeOfAcceptance).HasDefaultValue(null);
            builder.Property(a => a.CreatedAt).HasDefaultValue(null);
        });
    }
}