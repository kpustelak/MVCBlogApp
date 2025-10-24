namespace MVCBlogApp.Models.DTO.Post;

public class AddOrEditPostDto
{
    public string Title { get; set; } = String.Empty;
    public string Content { get; set; } = String.Empty;
    public string Slug { get; set; } =  String.Empty;
    public bool IsPublished { get; set; } = false;
}