using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Interface;

public interface IPostPublishingService
{
    Task AddPostAsync(AddOrEditPostDto addPostDto);
    Task<Post> EditPostAsync(AddOrEditPostDto newPostData, int postId);
    Task DeletePostAsync(int postId);
    Task<Post?> GetWholePostAsync(int postId);
}