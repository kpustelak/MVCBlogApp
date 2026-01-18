using MVCBlogApp.Model.DTO.PostImage;
using MVCBlogApp.Models.DTO.PostImage;

namespace MVCBlogApp.Models.ViewModel.Image;

public record ImageIndex(AddPostImageRequest PostImageRequest, 
    List<Models.PostImage> ListOfImages, 
    EditPostImageAltTextRequest EditAltTextRequest, 
    int? CurrentPage, 
    int? TotalPages, 
    int? PageSize, 
    int? TotalItems);