namespace MVCBlogApp.Models.ViewModel.Post;

using MVCBlogApp.Models.Entities;

public record PostIndex(List<Post> Posts, int TotalItems, int CurrentPage, int TotalPages);