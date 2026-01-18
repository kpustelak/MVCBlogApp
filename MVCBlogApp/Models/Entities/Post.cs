using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCBlogApp.Models.Entities;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public required string Title { get; set; }
    public string Content { get; set; } = string.Empty;
    public string Excerpt { get; set; } = string.Empty;
    public string Slug  { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
    public string UpdatedAt { get; set; } = string.Empty;
    public bool IsPublished { get; set; } = false;
    public bool? IsFavourite { get; set; } = false;
    public int ViewCount { get; set; } = 0;
    public int? FeaturedImageId { get; set; }
    public int? PostCategoryId { get; set; }
    public virtual PostCategory? PostCategory { get; set; }
    public virtual PostImage? FeaturedImage { get; set; }
}