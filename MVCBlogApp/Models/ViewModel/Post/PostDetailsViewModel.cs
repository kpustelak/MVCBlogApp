using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Models.ViewModel.Post;

public record PostDetailsViewModel(GetPostModelDto PostModel, List<PostCategory>? PostCategories);