namespace MVCBlogApp.Models.ViewModel.Post;
using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;


public record CategoryViewModel(int PageNumber, 
    int PageSize, 
    List<ShortPostModelDto> Posts, 
    PostCategory PostsCategory, 
    List<PostCategory>?  PostCategories);