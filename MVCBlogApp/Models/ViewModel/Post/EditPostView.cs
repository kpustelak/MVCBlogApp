using System.ComponentModel.DataAnnotations;
using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Models.ViewModel.Post;

public class EditPostView
{
    [Required]
    public List<PostCategory> PostCategories { get; set; } = new List<PostCategory>();
    [Required]
    public AddOrEditPostDto? PostDto { get; set; } = new AddOrEditPostDto();
    [Required]
    public int? EditedPostId { get; set; }
}