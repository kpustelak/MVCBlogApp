using MVCBlogApp.Migrations;
using PostCategory = MVCBlogApp.Models.Entities.PostCategory;

namespace MVCBlogApp.Models;

public class HomeViewModel
{
    public List<PostCategory> PostCategories { get; set; }
}