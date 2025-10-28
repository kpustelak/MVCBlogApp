using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Interface;
using MVCBlogApp.Models.DTO.Post;

namespace MVCBlogApp.Controllers;

public class PostPublishingController : Controller
{
    private readonly IPostPublishingService  _postPublishingService;
    public PostPublishingController(IPostPublishingService postPublishingService)
    {
        _postPublishingService = postPublishingService;
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
    public async Task<IActionResult> AddNewPostAsync(AddOrEditPostDto addPostDto)
    {
        await _postPublishingService.AddNewPostAsync(addPostDto);
        return RedirectToAction("Index");
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePostAsync(int postId)
    {
        await _postPublishingService.DeletePostAsync(postId);
        return RedirectToAction("Index");
    }
    [HttpGet("id")]
    public async Task<IActionResult> EditPost(int postId)
    {
        var post = await _postPublishingService.GetWholePostAsync(postId);
        var postToEdit = new AddOrEditPostDto
        {
            Title = post.Title,
            Content = post.Content,
            Slug = post.Slug,
            IsPublished = post.IsPublished
        };
        return View(postToEdit);
    }
    
    [HttpPut("id")]
    public async Task<IActionResult> EditPost(AddOrEditPostDto editPostDto,int postId)
    {
        var post = await _postPublishingService.EditPostAsync(editPostDto, postId);
        return Ok(post);
    }
}