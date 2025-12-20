using System.ComponentModel.DataAnnotations;

public class AddOrEditCategoryDto
{
    [Required]
    public string Name { get; set;}
    [Required]
    public string IconBootstrapLink { get; set; }
}