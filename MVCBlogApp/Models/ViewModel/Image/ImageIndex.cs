using MVCBlogApp.Models.DTO.PostImage;

namespace MVCBlogApp.Models.ViewModel.Image;

public class ImageIndex
{
    public AddPostImageRequest postImageRequest {get;set; }
    public List<Models.PostImage> listOfImages { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalItems { get; set; }
}