namespace MVCBlogApp.Models.Entities;

public class PostImage
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string FilePath { get; set; }
    public string AltText { get; set; }
}