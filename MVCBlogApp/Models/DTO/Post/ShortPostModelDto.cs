namespace MVCBlogApp.Models.DTO.Post;

public class ShortPostModelDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Slug  { get; set; }
    public string Excerpt { get; set; }
    public string CreatedAt { get; set; }
    public bool IsPublished { get; set; }
}