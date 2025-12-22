using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Interface;
using MVCBlogApp.Models;
using MVCBlogApp.Models.Entities;

namespace MVCBlogApp.Controllers;

public class HomeController : Controller
{
    private readonly ICategoryService _categoryService;
    public HomeController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetCategoriesAsync();
        var homeViewModel = new HomeViewModel
        {
            PostCategories = categories
        };
        return View(homeViewModel);
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}