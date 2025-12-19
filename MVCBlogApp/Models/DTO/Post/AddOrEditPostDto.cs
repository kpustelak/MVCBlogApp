using System.ComponentModel.DataAnnotations;

namespace MVCBlogApp.Models.DTO.Post;

public class AddOrEditPostDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    [Display(Name = "Post Title")]
    public string Title { get; set; } = String.Empty;
    
    [Display(Name = "Post Content")]
    public string? Content { get; set; } = String.Empty;
    
    [Display(Name = "URL Slug")]
    public string? Slug { get; set; } = String.Empty;
    
    [Display(Name = "Publish Post")]
    public bool IsPublished { get; set; } = false;
    
    [Required(ErrorMessage = "Please select a category")]
    [Display(Name = "Category")]
    public int PostCategoryId { get; set; }
    
    [Display(Name = "Featured Image")]
    public string? FeaturedImageUrl { get; set; }
}