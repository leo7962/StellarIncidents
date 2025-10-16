namespace StellarIncidents.Application.Dtos;

public class CommentDto
{
    public string Text { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}