namespace MVCBlogApp.Models.ViewModel.Category;

public record EditCategoryView(int? CategoryId, AddOrEditCategoryDto? CategoryDto)
{
    public bool IsCategoryEdited => CategoryId.HasValue && CategoryDto is not null;
}