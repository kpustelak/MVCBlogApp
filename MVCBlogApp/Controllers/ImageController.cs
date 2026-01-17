using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Interface;
using MVCBlogApp.Model.DTO.PostImage;
using MVCBlogApp.Models.DTO.PostImage;
using MVCBlogApp.Models.ViewModel.Image;

namespace MVCBlogApp.Controllers;

[Route("[controller]")]
public class ImageController : Controller
{
    private readonly IImageService _imageService;
    private readonly ILogger<ImageController> _logger;
    
    public ImageController(IImageService imageService, ILogger<ImageController> logger)
    {
        _imageService = imageService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int currentPage = 1)
    {
        try
        {
            var pageSize = 20;
            var totalItems = await _imageService.GetTotalImageCountAsync();
            var images = await _imageService.GetImageDataListAsync(currentPage, pageSize);

            return View(new ImageIndex(new AddPostImageRequest(), 
                images, 
                new EditPostImageAltTextRequest(), 
                currentPage, 
                (int)Math.Ceiling(totalItems / (double)pageSize), 
                pageSize, 
                totalItems));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading image index page {PageNumber}", currentPage);
            TempData["ErrorMessage"] = "An error occurred while loading images.";
            return RedirectToAction("Index", "Home");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadImage(AddPostImageRequest postImage)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Image upload failed - model validation error");
            TempData["ErrorMessage"] = "There is a validation error in your request.";
            return RedirectToAction(nameof(Index));
        }
        
        try
        {
            await _imageService.UploadImageAsync(postImage);
            _logger.LogInformation("Image uploaded successfully: {FileName}", postImage.File?.FileName);
            TempData["SuccessMessage"] = "File uploaded successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex) 
        {
            _logger.LogWarning(ex, "Invalid operation during image upload for file: {FileName}", postImage.File?.FileName);
            TempData["ErrorMessage"] = "There is some error in your request.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error during image upload for file: {FileName}", postImage.File?.FileName);
            TempData["ErrorMessage"] = "An unexpected error occurred while uploading the file.";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost("delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteImageAsync(int id)
    {
        if (id <= 0)
        {
            _logger.LogWarning("Image deletion attempted with invalid ID: {ImageId}", id);
            TempData["ErrorMessage"] = "Make sure your image ID is valid.";
            return RedirectToAction(nameof(Index));
        }
        
        try
        {
            await _imageService.DeleteImageAsync(id);
            _logger.LogInformation("Image deleted successfully: ID {ImageId}", id);
            TempData["SuccessMessage"] = "Image deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting image with ID: {ImageId}", id);
            TempData["ErrorMessage"] = "An error occurred while deleting the image.";
            return RedirectToAction(nameof(Index));
        }
    }

    [HttpPost("edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditImageAltTextAsync(EditPostImageAltTextRequest postImage)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Image alt text edit failed - model validation error");
            TempData["ErrorMessage"] = "There is a validation error in your request.";
            return RedirectToAction(nameof(Index));
        }

        if (!Int32.TryParse(postImage.fileId, out var fileId))
        {
            _logger.LogWarning("Image ID is not an int: {ImageId}", postImage.fileId);
            TempData["ErrorMessage"] = "Invalid image ID.";
            return RedirectToAction(nameof(Index));
        }
        

        if (fileId <= 0)
        {
            _logger.LogWarning("Image alt text edit attempted with invalid ID: {ImageId}", postImage.fileId);
            TempData["ErrorMessage"] = "Invalid image ID.";
            return RedirectToAction(nameof(Index));
        }

        try
        {
            await _imageService.EditImageAltTextAsync(fileId, postImage.fileAltText);
            _logger.LogInformation("Image alt text updated successfully for ID {ImageId}: '{AltText}'", 
                postImage.fileId, postImage.fileAltText);
            TempData["SuccessMessage"] = "Image alt text updated successfully.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error editing image alt text for ID: {ImageId}", postImage.fileId);
            TempData["ErrorMessage"] = "An error occurred while updating the image.";
            return RedirectToAction(nameof(Index));
        }
    }
}