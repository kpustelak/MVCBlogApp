using MVCBlogApp.Models.DTO.Category;

namespace MVCBlogApp.Interface;

public interface ICategoryManagmentService
{
    Task AddCategory(AddOrEditCategoryDto categoryToAdd);
    Task Delete(int categoryId);
    Task EditCategory(AddOrEditCategoryDto categoryToEdit, int categoryId);
}