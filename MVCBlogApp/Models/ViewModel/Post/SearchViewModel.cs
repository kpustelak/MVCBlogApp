using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Models.ViewModel.Post;

public class SearchViewModel
{
    public int PageNumber { get; set; }
    public int PageSize {get; set; }
    public List<ShortPostModelDto> Posts { get; set; } = new List<ShortPostModelDto>();
    public string Query { get; set; } = string.Empty;
    public List<PostCategory>?  PostCategories { get; set; }
}