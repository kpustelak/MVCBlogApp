using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Models.ViewModel.Post;

public class ByLatestsOrByViewsViewModel
{
    public int PageNumber { get; set; }
    public int PageSize {get; set; }
    public List<ShortPostModelDto> Posts { get; set; } = new List<ShortPostModelDto>();
    public bool ByViewsOnly { get; set; }
    public bool ByLatest { get; set; }
    public List<PostCategory>?  PostCategories { get; set; }
}