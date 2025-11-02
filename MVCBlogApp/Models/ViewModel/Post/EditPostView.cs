using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Models.ViewModel.Post;

public class EditPostView
{
    public List<PostCategory> PostCategories { get; set; } = new List<PostCategory>();
    public AddOrEditPostDto? PostDto { get; set; } = new AddOrEditPostDto();
    public int? EditedPostId { get; set; }
}