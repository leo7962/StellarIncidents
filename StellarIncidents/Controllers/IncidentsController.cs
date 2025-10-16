using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StellarIncidents.Application.Dtos;
using StellarIncidents.Domain.Entities;
using StellarIncidents.Domain.Interfaces;
using StellarIncidents.SwaggerExamples;
using Swashbuckle.AspNetCore.Filters;

namespace StellarIncidents.Controllers;

[Route("api/incidents")]
[ApiController]
public class IncidentsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IIncidentRepository _repository;

    public IncidentsController(IIncidentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<IncidentDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll()
    {
        var incidents = await _repository.ListAsync();
        var result = _mapper.Map<IEnumerable<IncidentDto>>(incidents);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(IncidentDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var incident = await _repository.GetByIdAsync(id);
        if (incident == null)
            return NotFound();

        var dto = _mapper.Map<IncidentDto>(incident);
        return Ok(dto);
    }

    [HttpPost]
    [SwaggerRequestExample(typeof(CreateIncidentDto), typeof(CreateIncidentExample))]
    [SwaggerResponseExample((int)HttpStatusCode.Created, typeof(IncidentResponseExample))]
    [ProducesResponseType(typeof(IncidentDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateIncidentDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
            return BadRequest("Title is required");

        var incident = new Incident
        {
            Title = dto.Title,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            ReporterUserId = dto.ReporterUserId,
            Status = IncidentStatus.Open
        };

        var created = await _repository.AddAsync(incident);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateIncidentDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return NotFound();

        existing.Title = dto.Title;
        existing.Description = dto.Description;
        existing.Status = dto.Status;
        existing.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(existing);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("{id:guid}/comments")]
    [ProducesResponseType(typeof(Comment), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> AddComment(Guid id, [FromBody] CommentCreateDto dto)
    {
        var comment = new Comment
        {
            AuthorUserId = dto.AuthorUserId,
            Text = dto.Text
        };

        try
        {
            var createdComment = await _repository.AddCommentAsync(id, comment);
            return Ok(createdComment);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}