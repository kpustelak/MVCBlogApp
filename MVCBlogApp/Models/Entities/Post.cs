using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBlogApp.Models.Entities;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    public string Content { get; set; } 
    public string Slug  { get; set; }
    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
    [Required]
    public bool IsPublished { get; set; }
    public int ViewCount { get; set; }
    public string FeaturedImageUrl { get; set; }
    public int PostCategoryId { get; set; }
    public virtual PostCategory PostCategory { get; set; }
}