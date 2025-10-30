using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Models.ViewModel;

public class NewPostView
{
    public List<PostCategory> PostCategories { get; set; } = new List<PostCategory>();
    public AddOrEditPostDto PostDto { get; set; } = new AddOrEditPostDto();
}