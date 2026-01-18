namespace MVCBlogApp.Models.Entities;

public class PostCategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string IconBootstrapLink { get; set; }
    public virtual List<Post> Posts { get; set; }
}