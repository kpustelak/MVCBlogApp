using System.ComponentModel.DataAnnotations;

namespace MVCBlogApp.Models;

public class PostImage
{
    public int Id { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string FileName { get; set; }
    
    [Required]
    [MaxLength(500)]
    public string FilePath { get; set; } 
    
    [MaxLength(200)]
    public string AltText { get; set; }
    
    public long FileSize { get; set; } 
    
    [MaxLength(10)]
    public string FileExtension { get; set; } 
    
    public DateTime UploadDate { get; set; }
}