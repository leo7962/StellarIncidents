namespace StellarIncidents.Domain.Entities;

public class Category
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;

    public ICollection<Incident> Incidents { get; set; } = new List<Incident>();
}