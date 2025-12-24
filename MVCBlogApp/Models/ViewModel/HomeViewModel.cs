using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;
using PostCategory = MVCBlogApp.Models.Entities.PostCategory;

namespace MVCBlogApp.Models;

public class HomeViewModel
{
    public List<PostCategory>? PostCategories { get; set; }
    public List<ShortPostModelDto>? PostsToDisplay { get; set; } 
}