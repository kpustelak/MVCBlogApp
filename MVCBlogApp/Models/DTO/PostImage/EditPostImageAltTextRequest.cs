
using System.ComponentModel.DataAnnotations;

namespace MVCBlogApp.Model.DTO.PostImage;

public class EditPostImageAltTextRequest
{
    [Required(ErrorMessage = "The field id is required")]
    public string fileId  { get; set; }
    [Required(ErrorMessage = "The field text is required")]
    public string fileAltText { get; set; }
}