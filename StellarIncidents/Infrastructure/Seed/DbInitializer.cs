using Microsoft.EntityFrameworkCore;
using StellarIncidents.Domain.Entities;

namespace StellarIncidents.Infrastructure.Seed;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Asegurar que la base de datos exista
        await context.Database.MigrateAsync();

        // Si ya existen categorías, no volver a insertar
        if (context.Categories.Any())
            return;

        var users = new List<User>
        {
            new() { Id = Guid.NewGuid(), FullName = "Leonardo Hernández", Email = "leonardo@example.com" },
            new() { Id = Guid.NewGuid(), FullName = "Admin Soporte", Email = "admin@example.com" },
            new() { Id = Guid.NewGuid(), FullName = "Usuario QA", Email = "qa@example.com" }
        };

        await context.Users.AddRangeAsync(users);

        var categories = new List<Category>
        {
            new() { Id = Guid.NewGuid(), Name = "Infraestructura" },
            new() { Id = Guid.NewGuid(), Name = "Seguridad" },
            new() { Id = Guid.NewGuid(), Name = "Software" }
        };

        await context.Categories.AddRangeAsync(categories);

        var incidents = new List<Incident>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Falla en servidor principal",
                Description = "El servidor dejó de responder desde las 3 AM.",
                CategoryId = categories.First(c => c.Name == "Infraestructura").Id,
                ReporterUserId = users.First(u => u.FullName == "Leonardo Hernández").Id,
                Status = IncidentStatus.Open,
                CreatedAt = DateTime.UtcNow.AddHours(-5),
                UpdatedAt = DateTime.UtcNow.AddHours(-4)
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "Intento de acceso no autorizado",
                Description = "Se detectó actividad sospechosa en el servidor de autenticación.",
                CategoryId = categories.First(c => c.Name == "Seguridad").Id,
                ReporterUserId = users.First(u => u.FullName == "Admin Soporte").Id,
                Status = IncidentStatus.InProgress,
                CreatedAt = DateTime.UtcNow.AddHours(-3),
                UpdatedAt = DateTime.UtcNow.AddHours(-2)
            }
        };

        await context.Incidents.AddRangeAsync(incidents);

        var comments = new List<Comment>
        {
            new()
            {
                Id = Guid.NewGuid(),
                IncidentId = incidents.First().Id,
                AuthorUserId = users.First(u => u.FullName == "Admin Soporte").Id,
                Text = "Revisando los logs del sistema.",
                CreatedAt = DateTime.UtcNow.AddHours(-3)
            },
            new()
            {
                Id = Guid.NewGuid(),
                IncidentId = incidents.Last().Id,
                AuthorUserId = users.First(u => u.FullName == "Usuario QA").Id,
                Text = "Confirmando el reporte de seguridad.",
                CreatedAt = DateTime.UtcNow.AddHours(-2)
            }
        };

        await context.Comments.AddRangeAsync(comments);

        await context.SaveChangesAsync();
    }
}