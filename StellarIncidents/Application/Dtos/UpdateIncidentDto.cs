using StellarIncidents.Domain.Entities;

namespace StellarIncidents.Application.Dtos;

public class UpdateIncidentDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IncidentStatus Status { get; set; }
}