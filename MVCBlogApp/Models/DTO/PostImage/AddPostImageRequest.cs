using System.ComponentModel.DataAnnotations;

namespace MVCBlogApp.Models.DTO.PostImage;

public class AddPostImageRequest
{
    [Required(ErrorMessage = "Tekst alternatywny jest wymagany")]
    [MaxLength(200)] 
    public string AltText { get; set; }
    
    [Required(ErrorMessage = "Plik jest wymagany")]
    [DataType(DataType.Upload)]
    public IFormFile File { get; set; } 
}