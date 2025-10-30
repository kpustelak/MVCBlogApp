using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Interface;
using MVCBlogApp.Models;
using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.ViewModel;

namespace MVCBlogApp.Controllers;

public class PostPublishingController : Controller
{
    private readonly IPostPublishingService  _postPublishingService;
    private readonly ICategoryService _categoryService;
    public PostPublishingController(IPostPublishingService postPublishingService,
        ICategoryService categoryService)
    {
        _postPublishingService = postPublishingService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> NewPost()
    {
        return View(new NewPostView { PostCategories = await _categoryService.GetCategories() });
    }

    [HttpPost]
    public async Task<IActionResult> AddNewPostAsync(NewPostView postView)
    {
        await _postPublishingService.AddNewPostAsync(postView.PostDto);
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
            IsPublished = post.IsPublished,
            PostCategoryId = post.PostCategoryId
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