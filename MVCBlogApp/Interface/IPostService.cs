using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Interface;

public interface IPostService
{
    Task<List<ShortPostModelDto>> GetListOfPostsWithPaginationAsync(int pageNumber, int pageSize);
    Task<List<ShortPostModelDto>> GetListOfPostsAsync(int top, bool pickFavourite);
}