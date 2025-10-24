using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Interface;
using MVCBlogApp.Models.DTO.Post;

namespace MVCBlogApp.Controllers;

public class PostPublishingController : Controller
{
    private readonly IPostService  _postService;
    public PostPublishingController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult NewPost()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddNewPost(AddOrEditPostDto addPostDto)
    {
        await _postService.AddNewPostAsync(addPostDto);
        return RedirectToAction("Index");
    }
    
}