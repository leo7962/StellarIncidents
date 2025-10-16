using StellarIncidents.Application.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace StellarIncidents.SwaggerExamples;

public class IncidentResponseExample : IExamplesProvider<IncidentDto>
{
    public IncidentDto GetExamples()
    {
        return new IncidentDto
        {
            Id = Guid.NewGuid(),
            Title = "Servidor de base de datos caído",
            Description = "El servidor SQL fue reiniciado correctamente.",
            CategoryName = "Infraestructura",
            ReporterName = "Leonardo Hernández",
            Status = "Closed",
            CreatedAt = DateTime.UtcNow.AddHours(-3),
            UpdatedAt = DateTime.UtcNow
        };
    }
}