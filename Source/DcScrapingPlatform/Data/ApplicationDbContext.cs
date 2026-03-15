using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DcScrapingPlatform.Models;

namespace DcScrapingPlatform.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Profile> Profiles { get; set; }
    public DbSet<DcCredential> DcCredentials { get; set; }
    public DbSet<AssetHistory> AssetHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Profile と ApplicationUser の 1:1 関係
        builder.Entity<ApplicationUser>()
            .HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<Profile>(p => p.UserId);

        // DcCredential と ApplicationUser の 1:1 関係
        builder.Entity<DcCredential>()
            .HasOne(c => c.User)
            .WithOne()
            .HasForeignKey<DcCredential>(c => c.UserId);

        // AssetHistory と ApplicationUser の 1:N 関係
        builder.Entity<AssetHistory>()
            .HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId);
    }
}
