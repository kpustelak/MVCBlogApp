namespace MVCBlogApp.Models.Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; } 
    public string Slug  { get; set; }
    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
    public bool IsPublished { get; set; }
    public int ViewCount { get; set; }
    
    public int PostCategoryId { get; set; }
    public virtual PostCategory PostCategory { get; set; }
}