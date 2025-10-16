namespace StellarIncidents.Application.Dtos;

public class CommentCreateDto
{
    public Guid AuthorUserId { get; set; }
    public string Text { get; set; } = string.Empty;
}