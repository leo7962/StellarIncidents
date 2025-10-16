namespace StellarIncidents.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public ICollection<Incident> ReportedIncidents { get; set; } = new List<Incident>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}