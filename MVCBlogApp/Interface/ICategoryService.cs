using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Interface;

public interface ICategoryService
{
    Task<List<PostCategory>> GetCategoriesAsync();
    Task<PostCategory?> GetCategoryByIdAsync(int id);
}