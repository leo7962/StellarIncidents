namespace StellarIncidents.Domain.Entities;

public class Incident
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public Guid ReporterUserId { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public IncidentStatus Status { get; set; } = IncidentStatus.Open;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Category Category { get; set; } = null!;
    public User ReporterUser { get; set; } = null!;
    public User? AssignedToUser { get; set; }

    public List<Comment> Comments { get; set; } = new();
}

public enum IncidentStatus
{
    Open,
    InProgress,
    Closed
}