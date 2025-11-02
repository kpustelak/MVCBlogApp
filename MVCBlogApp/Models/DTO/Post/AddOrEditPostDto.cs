using System.ComponentModel.DataAnnotations;

namespace MVCBlogApp.Models.DTO.Post;

public class AddOrEditPostDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    [Display(Name = "Post Title")]
    public string Title { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Content is required")]
    [MinLength(10, ErrorMessage = "Content must be at least 10 characters")]
    [Display(Name = "Post Content")]
    public string Content { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Slug is required")]
    [StringLength(100, ErrorMessage = "Slug cannot exceed 100 characters")]
    [RegularExpression(@"^[a-z0-9]+(?:-[a-z0-9]+)*$", 
        ErrorMessage = "Slug must be lowercase with hyphens (e.g., my-post-slug)")]
    [Display(Name = "URL Slug")]
    public string Slug { get; set; } = String.Empty;
    
    [Display(Name = "Publish Post")]
    public bool IsPublished { get; set; } = false;
    
    [Required(ErrorMessage = "Please select a category")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
    [Display(Name = "Category")]
    public int PostCategoryId { get; set; }
}