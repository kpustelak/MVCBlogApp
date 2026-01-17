using MVCBlogApp.Models.DTO.Post;
using PostCategory = MVCBlogApp.Models.Entities.PostCategory;
namespace MVCBlogApp.Models;
public record HomeViewModel(List<PostCategory>? PostCategories, List<ShortPostModelDto>? PostsToDisplay);