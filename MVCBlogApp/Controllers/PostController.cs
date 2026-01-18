using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Interface;
using MVCBlogApp.Models.ViewModel.Post;

namespace MVCBlogApp.Controllers;

public class PostController : Controller
{
    private readonly IPostService _context;
    private readonly ICategoryService _categoryService;
    private readonly ILogger<PostController> _logger;
    
    public PostController(IPostService context,
        ICategoryService categoryService,
        ILogger<PostController> logger)
    {
        _context = context;
        _categoryService = categoryService;
        _logger = logger;
    }
    
    public async Task<IActionResult> Index(int id)
    {
        if (id <= 0)
        {
            return RedirectToAction("Index", "Home");
        }
        try
        {
            _logger.LogInformation("Loading post details for ID: {PostId}", id);
            return View(new PostDetailsViewModel(await _context.GetPostByIdAsync(id), await _categoryService.GetCategoriesAsync()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading post ID: {PostId}", id);
            throw new Exception(ex.Message);
        }
    }
    
    public async Task<IActionResult> Category(int categoryId, int pageNumber=0)
    { 
        int pageSize = 10;
        try
        {
            _logger.LogInformation("Loading category {CategoryId}, page {PageNumber}", categoryId, pageNumber);
            return View(new CategoryViewModel(pageNumber, 
                pageSize, 
                await _context.GetListOfPostsWithPaginationAndCategoryAsync(pageNumber, pageSize, categoryId),
                await _categoryService.GetCategoryByIdAsync(categoryId),
                await _categoryService.GetCategoriesAsync()));
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error loading category {CategoryId}, page {PageNumber}", categoryId, pageNumber);
            throw new Exception(ex.Message);
        }
    }

    public async Task<IActionResult> ByLatestsOrByViews(int pageNumber=0, bool byLatest = false, bool byViews = false)
    {
        int pageSize = 10;
        try
        {
            if (byLatest == byViews)
            {
                _logger.LogWarning("Invalid sorting params: byLatest={ByLatest}, byViews={ByViews}", byLatest, byViews);
                throw new Exception("There is only one option for post sorting.");
            }
            _logger.LogInformation("Loading posts page {PageNumber}, byLatest={ByLatest}, byViews={ByViews}", pageNumber, byLatest, byViews);
            var vm = new ByLatestsOrByViewsViewModel
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Posts = await _context.GetListOfPostsWithPaginationAsync(pageNumber, pageSize, byLatest, byViews),
                PostCategories = await _categoryService.GetCategoriesAsync(),
                ByViewsOnly = byViews,
                ByLatest = byLatest
            };
            return View(vm);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error loading posts page {PageNumber}", pageNumber);
            throw new Exception(ex.Message);
        }
    }

    public async Task<IActionResult> SearchResult(string query, int pageNumber = 1)
    {
        int pageSize = 10;
        try
        {
            _logger.LogInformation("Searching for '{Query}', page {PageNumber}", query, pageNumber);
            var vm = new SearchViewModel()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Posts = await _context.GetListOfPostsByQueryAsync(pageNumber, pageSize, query),
                PostCategories = await _categoryService.GetCategoriesAsync(),
                Query = query,
            };
            return View(vm);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Error searching for '{Query}', page {PageNumber}", query, pageNumber);
            throw new Exception(ex.Message);
        }
    }
}