namespace MVCBlogApp.Models.ViewModel.Post;

using MVCBlogApp.Models.Entities;

public class PostIndex
{
    public List<Post> Posts { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages  { get; set; }
}