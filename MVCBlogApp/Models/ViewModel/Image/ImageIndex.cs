using MVCBlogApp.Models.DTO.PostImage;

namespace MVCBlogApp.Models.ViewModel.Image;

public class ImageIndex
{
    public AddPostImageRequest postImageRequest {get;set; }
    public List<Models.PostImage> listOfImages { get; set; }
    
}