using Microsoft.EntityFrameworkCore;
using StellarIncidents.Domain.Entities;
using StellarIncidents.Domain.Interfaces;

namespace StellarIncidents.Infrastructure.Repositories;

public class IncidentRepository : IIncidentRepository
{
    private readonly AppDbContext _db;

    public IncidentRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Incident> AddAsync(Incident incident, CancellationToken ct = default)
    {
        _db.Incidents.Add(incident);
        await _db.SaveChangesAsync(ct);
        return incident;
    }

    public async Task<Comment> AddCommentAsync(Guid incidentId, Comment comment, CancellationToken ct = default)
    {
        var incident = await _db.Incidents.FindAsync(new object[] { incidentId }, ct);
        if (incident == null)
            throw new InvalidOperationException("Incident not found.");

        // Asocia y guarda el comentario directamente
        comment.IncidentId = incidentId;
        _db.Comments.Add(comment);
        await _db.SaveChangesAsync(ct);

        return comment;
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var incident = await _db.Incidents.FindAsync(new object[] { id }, ct);
        if (incident != null)
        {
            _db.Incidents.Remove(incident);
            await _db.SaveChangesAsync(ct);
        }
    }

    public async Task<Incident?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _db.Incidents
        .Include(i => i.Category)
        .Include(i => i.ReporterUser)
        .Include(i => i.Comments)
            .ThenInclude(c => c.AuthorUser)
        .AsNoTracking()
        .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<List<Incident>> ListAsync(CancellationToken ct = default)
    {
        return await _db.Incidents
        .Include(i => i.Category)
        .Include(i => i.ReporterUser)
        .Include(i => i.Comments)
            .ThenInclude(c => c.AuthorUser)
        .AsNoTracking()
        .ToListAsync();
    }

    public async Task UpdateAsync(Incident incident, CancellationToken ct = default)
    {
        incident.UpdatedAt = DateTime.UtcNow;
        _db.Incidents.Update(incident);
        await _db.SaveChangesAsync(ct);
    }
}