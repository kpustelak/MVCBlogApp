using System.ComponentModel.DataAnnotations;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Models.ViewModel.Category;

public record CategoryIndex(List<PostCategory>? PostCategories);