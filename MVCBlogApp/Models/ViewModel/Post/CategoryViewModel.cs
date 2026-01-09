namespace MVCBlogApp.Models.ViewModel.Post;
using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

class CategoryViewModel()
{
    public int PageNumber { get; set; }
    public int PageSize {get; set; }
    public List<ShortPostModelDto> Posts { get; set; } = new List<ShortPostModelDto>();
    public PostCategory PostsCategory { get; set; } = new PostCategory();
}