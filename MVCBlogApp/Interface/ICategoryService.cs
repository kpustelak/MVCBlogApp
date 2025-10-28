using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Interface;

public interface ICategoryService
{
    Task<List<PostCategory>> GetCategories();
}