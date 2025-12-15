using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Interface;

public interface IPostPublishingService
{
    Task<Post> AddPostAsync(AddOrEditPostDto addPostDto);
    Task DeletePostAsync(int postId);
    Task<Post> GetWholePostAsync(int postId); 
    Task<Post> EditPostAsync(AddOrEditPostDto newPostData, int postId);
}