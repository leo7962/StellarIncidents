namespace StellarIncidents.Application.Dtos;

public class CreateIncidentDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public Guid ReporterUserId { get; set; }
}