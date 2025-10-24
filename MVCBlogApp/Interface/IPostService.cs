using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Interface;

public interface IPostService
{
    Task AddNewPostAsync(AddOrEditPostDto addPostDto);
    Task<Post> EditPostAsync(AddOrEditPostDto newPostData, int postId);
    Task<Post> GetWholePostAsync(int postId);
    Task<List<ShortPostModelDto>> GetListOfPostsWithPaginationAsync(int pageNumber, int pageSize);
    Task DeletePostAsync(int postId);
    Task<List<ShortPostModelDto>> GetListOfPostsAsync(int top);
}