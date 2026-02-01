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
    private readonly IConfiguration _configuration;
    public HomeController(ICategoryService categoryService, IPostService postService, IConfiguration configuration)
    {
        _categoryService = categoryService;
        _postService = postService;
        _configuration = configuration;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(new HomeViewModel(
            await _categoryService.GetCategoriesAsync() ,
            await _postService.GetListOfPostsAsync(4, true)));
    }
}