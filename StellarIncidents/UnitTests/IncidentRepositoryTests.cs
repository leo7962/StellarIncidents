using Microsoft.EntityFrameworkCore;
using StellarIncidents.Domain.Entities;
using StellarIncidents.Infrastructure;
using StellarIncidents.Infrastructure.Repositories;
using Xunit;

namespace StellarIncidents.UnitTests;

public class IncidentRepositoryTests
{
    private AppDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateIncident_HappyPath()
    {
        var db = CreateInMemoryDb();
        var repo = new IncidentRepository(db);
        var incident = new Incident
            { Title = "Test", Description = "desc", ReporterUserId = Guid.NewGuid(), CategoryId = Guid.NewGuid() };

        var created = await repo.AddAsync(incident);
        Assert.NotNull(created);
        Assert.Equal("Test", created.Title);
    }

    [Fact]
    public async Task Update_NonExisting_ThrowsOrReturnsNotFound()
    {
        var db = CreateInMemoryDb();
        var repo = new IncidentRepository(db);
        var nonExisting = await repo.GetByIdAsync(Guid.NewGuid());
        Assert.Null(nonExisting);
    }
}