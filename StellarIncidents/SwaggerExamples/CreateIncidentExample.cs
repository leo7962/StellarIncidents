using StellarIncidents.Application.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace StellarIncidents.SwaggerExamples;

public class CreateIncidentExample : IExamplesProvider<CreateIncidentDto>
{
    public CreateIncidentDto GetExamples()
    {
        return new CreateIncidentDto
        {
            Title = "Servidor de base de datos caído",
            Description = "El servidor principal SQL dejó de responder desde las 2:00 AM.",
            CategoryId = Guid.Parse("b84f9cf2-2d2d-4cf9-9969-b88f73b6d4d2"),
            ReporterUserId = Guid.Parse("0f6f6c7c-5f8d-4b2d-8e7a-cdf8e2b0eae2")
        };
    }
}