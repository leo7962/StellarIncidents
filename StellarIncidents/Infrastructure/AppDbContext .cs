using Microsoft.EntityFrameworkCore;
using StellarIncidents.Domain.Entities;

namespace StellarIncidents.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Incident> Incidents => Set<Incident>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Incident -> Comments
        modelBuilder.Entity<Incident>()
            .HasMany(i => i.Comments)
            .WithOne(c => c.Incident)
            .HasForeignKey(c => c.IncidentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Incident -> Category
        modelBuilder.Entity<Incident>()
            .HasOne(i => i.Category)
            .WithMany(c => c.Incidents)
            .HasForeignKey(i => i.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Incident -> ReporterUser
        modelBuilder.Entity<Incident>()
            .HasOne(i => i.ReporterUser)
            .WithMany(u => u.ReportedIncidents)
            .HasForeignKey(i => i.ReporterUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Incident -> AssignedToUser (opcional)
        modelBuilder.Entity<Incident>()
            .HasOne(i => i.AssignedToUser)
            .WithMany()
            .HasForeignKey(i => i.AssignedToUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Comment -> AuthorUser
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.AuthorUser)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.AuthorUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Propiedades y longitudes (ejemplo)
        modelBuilder.Entity<Incident>()
            .Property(i => i.Title)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<User>()
            .Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("9d2ae144-8a49-4a44-aa4e-081c6526ab33"), FullName = "Leonardo Hernández",
                Email = "leonardo@example.com"
            },
            new User
            {
                Id = Guid.Parse("adb396b6-416e-420e-8b37-46a2c07f8e93"), FullName = "Admin Soporte",
                Email = "soporte@example.com"
            }
        );
    }
}