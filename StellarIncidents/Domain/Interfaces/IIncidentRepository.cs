using StellarIncidents.Domain.Entities;

namespace StellarIncidents.Domain.Interfaces;

public interface IIncidentRepository
{
    Task<Incident> AddAsync(Incident incident, CancellationToken ct = default);
    Task<Incident?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task UpdateAsync(Incident incident, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task<List<Incident>> ListAsync(CancellationToken ct = default);
    Task<Comment> AddCommentAsync(Guid incidentId, Comment comment, CancellationToken ct = default);
}