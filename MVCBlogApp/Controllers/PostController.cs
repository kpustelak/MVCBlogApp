using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Interface;
using MVCBlogApp.Models.ViewModel.Post;
using MVCBlogApp.Service;

namespace MVCBlogApp.Controllers;

public class PostController : Controller
{
    private readonly IPostService _context;
    private readonly ICategoryService _categoryService;
    public PostController(IPostService context,
        ICategoryService categoryService)
    {
        _context = context;
        _categoryService = categoryService;
    }
    public async Task<IActionResult> Index(int id)
    {
        try
        {
            return View(new PostDetailsViewModel
            {
                PostModel = await _context.GetPostByIdAsync(id),
                PostCategories = await _categoryService.GetCategoriesAsync()
            });
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}