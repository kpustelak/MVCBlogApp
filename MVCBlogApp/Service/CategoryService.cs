using Microsoft.EntityFrameworkCore;
using MVCBlogApp.Db;
using MVCBlogApp.Interface;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Service;

public class CategoryService : ICategoryService
{
    private readonly BlogDbContext  _context;
    public CategoryService(BlogDbContext context)
    {
        _context = context;
    }
    public async Task<List<PostCategory>> GetCategoriesAsync()
    {
        return await _context.PostCategories.ToListAsync();
    }

    public async Task<PostCategory?> GetCategoryByIdAsync(int id)
    {
        return await _context.PostCategories.FirstOrDefaultAsync(x => x.Id == id);
    }
}