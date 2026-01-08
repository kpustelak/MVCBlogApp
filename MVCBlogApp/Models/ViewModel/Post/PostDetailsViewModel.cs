using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Models.ViewModel.Post;

public class PostDetailsViewModel
{
    public GetPostModelDto PostModel  { get; set; } = new GetPostModelDto();
    public List<PostCategory>? PostCategories  { get; set; }
}