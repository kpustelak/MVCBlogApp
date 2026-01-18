using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Models.DTO.Post;

public class GetPostModelDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Slug  { get; set; }
    public string CreatedAt { get; set; } = string.Empty;
    public virtual PostCategory? PostCategory { get; set; }
    public virtual Models.PostImage? FeaturedImage { get; set; }
}