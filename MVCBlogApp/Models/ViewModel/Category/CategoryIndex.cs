using System.ComponentModel.DataAnnotations;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Models.ViewModel.Category;

public class CategoryIndex
{
    public List<PostCategory> Categories {get;set;} 
    [Required]
    public AddOrEditCategoryDto CategoryToEdit {get; set;}
    public int? CategoryId {get; set;}

        public CategoryIndex(List<PostCategory> categories,
                         AddOrEditCategoryDto? categoryToEdit,
                         int? categoryId)
    {
        Categories = categories == null ? [] : categories ;
        CategoryToEdit = categoryToEdit == null ? new AddOrEditCategoryDto() : categoryToEdit;
        CategoryId = categoryId == null ? 0 : categoryId;
    }
}