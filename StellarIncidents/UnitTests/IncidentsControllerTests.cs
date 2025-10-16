using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StellarIncidents.Application.Dtos;
using StellarIncidents.Controllers;
using StellarIncidents.Domain.Entities;
using StellarIncidents.Domain.Interfaces;
using Xunit;

namespace StellarIncidents.UnitTests;

public class IncidentsControllerTests
{
    private readonly IncidentsController _controller;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IIncidentRepository> _mockRepo;

    public IncidentsControllerTests()
    {
        _mockRepo = new Mock<IIncidentRepository>();
        _mockMapper = new Mock<IMapper>();
        _controller = new IncidentsController(_mockRepo.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithListOfIncidentDtos()
    {
        // Arrange
        var incidents = new List<Incident>
        {
            new() { Id = Guid.NewGuid(), Title = "Incident 1" },
            new() { Id = Guid.NewGuid(), Title = "Incident 2" }
        };

        var dtos = new List<IncidentDto>
        {
            new() { Id = incidents[0].Id, Title = "Incident 1" },
            new() { Id = incidents[1].Id, Title = "Incident 2" }
        };

        _mockRepo.Setup(r => r.ListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(incidents);
        _mockMapper.Setup(m => m.Map<IEnumerable<IncidentDto>>(incidents)).Returns(dtos);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<IncidentDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count());
    }

    // ✅ GET BY ID - FOUND
    [Fact]
    public async Task GetById_ReturnsOk_WhenIncidentExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var incident = new Incident { Id = id, Title = "Test Incident" };
        var dto = new IncidentDto { Id = id, Title = "Test Incident" };

        _mockRepo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(incident);
        _mockMapper.Setup(m => m.Map<IncidentDto>(incident)).Returns(dto);

        // Act
        var result = await _controller.GetById(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<IncidentDto>(okResult.Value);
        Assert.Equal(id, returnValue.Id);
        Assert.Equal("Test Incident", returnValue.Title);
    }

    // ✅ GET BY ID - NOT FOUND
    [Fact]
    public async Task GetById_ReturnsNotFound_WhenIncidentDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        _mockRepo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync((Incident?)null);

        // Act
        var result = await _controller.GetById(id);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    // ✅ CREATE - BAD REQUEST
    [Fact]
    public async Task Create_ReturnsBadRequest_WhenTitleIsEmpty()
    {
        // Arrange
        var dto = new CreateIncidentDto { Title = "" };

        // Act
        var result = await _controller.Create(dto);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Title is required", badRequest.Value);
    }

    // ✅ CREATE - SUCCESS
    [Fact]
    public async Task Create_ReturnsCreated_WhenValidDto()
    {
        // Arrange
        var dto = new CreateIncidentDto
        {
            Title = "New Incident",
            Description = "Description",
            CategoryId = Guid.NewGuid(),
            ReporterUserId = Guid.NewGuid()
        };

        var createdIncident = new Incident
        {
            Id = Guid.NewGuid(),
            Title = dto.Title
        };

        _mockRepo.Setup(r => r.AddAsync(It.IsAny<Incident>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(createdIncident);

        // Act
        var result = await _controller.Create(dto);

        // Assert
        var createdAt = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<Incident>(createdAt.Value);
        Assert.Equal(dto.Title, returnValue.Title);
    }

    // ✅ UPDATE - NOT FOUND
    [Fact]
    public async Task Update_ReturnsNotFound_WhenIncidentNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dto = new UpdateIncidentDto
        {
            Title = "Updated",
            Description = "Desc",
            Status = IncidentStatus.Open
        };

        _mockRepo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Incident?)null);

        // Act
        var result = await _controller.Update(id, dto);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    // ✅ UPDATE - SUCCESS
    [Fact]
    public async Task Update_ReturnsNoContent_WhenUpdatedSuccessfully()
    {
        // Arrange
        var id = Guid.NewGuid();
        var existing = new Incident { Id = id, Title = "Old" };
        var dto = new UpdateIncidentDto
        {
            Title = "Updated",
            Description = "Desc",
            Status = IncidentStatus.Closed
        };

        _mockRepo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(existing);

        // Act
        var result = await _controller.Update(id, dto);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(r => r.UpdateAsync(It.Is<Incident>(i => i.Title == "Updated"), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    // ✅ DELETE
    [Fact]
    public async Task Delete_ReturnsNoContent_WhenDeletedSuccessfully()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var result = await _controller.Delete(id);

        // Assert
        Assert.IsType<NoContentResult>(result);
        _mockRepo.Verify(r => r.DeleteAsync(id, It.IsAny<CancellationToken>()), Times.Once);
    }

    // ✅ ADD COMMENT - NOT FOUND
    [Fact]
    public async Task AddComment_ReturnsNotFound_WhenIncidentDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dto = new CommentCreateDto
        {
            AuthorUserId = Guid.NewGuid(),
            Text = "Comentario inexistente"
        };

        _mockRepo.Setup(r => r.AddCommentAsync(id, It.IsAny<Comment>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("Incident not found."));

        // Act
        var result = await _controller.AddComment(id, dto);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    // ✅ ADD COMMENT - SUCCESS
    [Fact]
    public async Task AddComment_ReturnsOk_WhenCommentIsAddedSuccessfully()
    {
        // Arrange
        var id = Guid.NewGuid();
        var dto = new CommentCreateDto
        {
            AuthorUserId = Guid.NewGuid(),
            Text = "Nuevo comentario"
        };

        var expectedComment = new Comment
        {
            Id = Guid.NewGuid(),
            IncidentId = id,
            AuthorUserId = dto.AuthorUserId,
            Text = dto.Text,
            CreatedAt = DateTime.UtcNow
        };

        _mockRepo.Setup(r => r.AddCommentAsync(id, It.IsAny<Comment>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedComment);

        // Act
        var result = await _controller.AddComment(id, dto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Comment>(okResult.Value);
        Assert.Equal(dto.Text, returnValue.Text);
        Assert.Equal(dto.AuthorUserId, returnValue.AuthorUserId);
        _mockRepo.Verify(r => r.AddCommentAsync(id, It.IsAny<Comment>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}