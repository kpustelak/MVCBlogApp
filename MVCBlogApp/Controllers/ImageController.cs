using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVCBlogApp.Interface;
using MVCBlogApp.Models;
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
    public async Task<IActionResult> Index(int page = 1)
    {
        var pageSize = 20;
        var totalItems = await _imageService.GetTotalImageCountAsync(); // Dodaj tę metodę w serwisie
        var images = await _imageService.GetImageDataListAsync(page, pageSize);
        
        var vm = new ImageIndex
        {
            listOfImages = images,
            postImageRequest = new AddPostImageRequest(),
            CurrentPage = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize)
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UploadImage(AddPostImageRequest postImage)
    {
        if (!ModelState.IsValid)
        {
            
            TempData["ErrorMessage"] = "There is validation error in your request.";
            return RedirectToAction(nameof(Index));
        }
        
        try
        {
            await _imageService.UploadImageAsync(postImage);
            TempData["SuccessMessage"] = "File uploaded successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex) 
        {
            TempData["ErrorMessage"] = "There is some error in your request";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    [HttpPost("{id}")]
    [IgnoreAntiforgeryToken]
    public async Task<IActionResult> DeleteImageAsync(int id)
    {
        if (id <= 0)
        {
            TempData["ErrorMessage"] = "Make sure your image id is valid";
            return RedirectToAction(nameof(Index));
        }
        try
        {
            await _imageService.DeleteImageAsync(id);
            TempData["SuccessMessage"] = "Image deleted successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}