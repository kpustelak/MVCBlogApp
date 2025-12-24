using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Interface;
using MVCBlogApp.Models;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Controllers;

public class HomeController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IPostService _postService;
    public HomeController(ICategoryService categoryService, IPostService postService)
    {
        _categoryService = categoryService;
        _postService = postService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        var homeViewModel = new HomeViewModel
        {
            PostCategories = categories,
            PostsToDisplay = await _postService.GetListOfFavouritePublicPosts(4)
        };
        return View(homeViewModel);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}