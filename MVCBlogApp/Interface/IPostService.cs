using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Interface;

public interface IPostService
{
    Task<List<ShortPostModelDto>> GetListOfPostsWithPaginationAsync(int pageNumber, int pageSize, bool byLatests, bool byViews);
    Task<List<ShortPostModelDto>> GetListOfPostsAsync(int top, bool pickFavourite);
    Task<List<ShortPostModelDto>> GetListOfPostsWithPaginationAndCategoryAsync(int pageNumber, int pageSize, int categoryId);
    Task<GetPostModelDto> GetPostByIdAsync(int postId);
    Task<List<ShortPostModelDto>> GetListOfPostsByQueryAsync(int pageNumber, int pageSize, string query);
}