using System.ComponentModel.DataAnnotations;

public class AddOrEditCategoryDto
{
    [Required]
    public string Name { get; set;} = string.Empty;
    [Required]
    public string IconBootstrapLink { get; set; } = string.Empty;
}