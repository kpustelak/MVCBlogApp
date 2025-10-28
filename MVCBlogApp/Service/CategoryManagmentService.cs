using Microsoft.EntityFrameworkCore;
using MVCBlogApp.Db;
using MVCBlogApp.Interface;
using MVCBlogApp.Models.DTO.Category;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Service;

public class CategoryManagmentService : ICategoryManagmentService
{
    private readonly BlogDbContext  _context;
    public CategoryManagmentService(BlogDbContext context)
    {
        _context = context;
    }
    
    public async Task AddCategory(AddOrEditCategoryDto categoryToAdd)
    {
        var newCategory = new PostCategory
        {
            Name = categoryToAdd.Name,
            IconBootstrapLink = categoryToAdd.IconBootstrapLink,
        };
        await _context.PostCategories.AddAsync(newCategory);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int categoryId)
    {
        var category = await _context.PostCategories.FirstOrDefaultAsync();
        if(category == null)
            throw new Exception("Category not found");
        _context.PostCategories.Remove(category);
        await _context.SaveChangesAsync();
    }

    public async Task EditCategory(AddOrEditCategoryDto categoryToEdit, int categoryId)
    {
        var category = await _context.PostCategories.FirstOrDefaultAsync(category => category.Id == categoryId);
        if(category == null)
            throw new Exception("Category not found");
        category.Name = categoryToEdit.Name;
        category.IconBootstrapLink = categoryToEdit.IconBootstrapLink;
        _context.PostCategories.Update(category);
        await _context.SaveChangesAsync();
    }
}