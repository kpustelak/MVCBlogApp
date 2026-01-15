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
    
    public async Task<IActionResult> Category(int categoryId, int pageNumber=0)
    {
        int pageSize = 10;
        try
        {
            var vm = new CategoryViewModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Posts = await _context.GetListOfPostsWithPaginationAndCategoryAsync(pageNumber, pageSize, categoryId),
                PostsCategory = await _categoryService.GetCategoryByIdAsync(categoryId),
                PostCategories = await _categoryService.GetCategoriesAsync()
            };
            return View(vm);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<IActionResult> ByLatestsOrByViews(int pageNumber=0, bool byLatests = false, bool byViews = false)
    {
        int pageSize = 10;
        try
        {
            if (byLatests == byViews)
            {
                throw new Exception("There is only one option for post sorting.");
            }
            var vm = new CategoryViewModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Posts = await _context.GetListOfPostsWithPaginationAsync(pageNumber, pageSize, byLatests, byViews),
                PostCategories = await _categoryService.GetCategoriesAsync()
            };
            return View(vm);
        }
        catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}