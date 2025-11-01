using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Interface;
using MVCBlogApp.Models;
using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.ViewModel;
using MVCBlogApp.Models.ViewModel.Post;

namespace MVCBlogApp.Controllers;

public class PostPublishingController : Controller
{
    private readonly IPostPublishingService  _postPublishingService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    private readonly ILogger<PostPublishingController> _logger;
    public PostPublishingController(IPostPublishingService postPublishingService,
        ICategoryService categoryService,
        IMapper mapper,
        ILogger<PostPublishingController> logger)
    {
        _postPublishingService = postPublishingService;
        _categoryService = categoryService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Route("PostPublishing/Edit")] 
    public async Task<IActionResult> Edit(int? postId) {
        if (postId is > 0) {
            var post = await _postPublishingService.GetWholePostAsync(postId.Value);
            if (post != null) {
                return View(new EditPostView {
                    PostCategories = await _categoryService.GetCategories(),
                    EditedPostId = postId,
                    PostDto = _mapper.Map<AddOrEditPostDto>(post)
                });
            }
        }
        return View(new EditPostView { PostCategories = await _categoryService.GetCategories() });
    }
    
    [HttpPost]
    [Route("PostPublishing/Save")] 
    public async Task<IActionResult> Save([FromForm] EditPostView model)
    {
        if (!ModelState.IsValid) {
            model.PostCategories = await _categoryService.GetCategories();
            return View("Edit", model);
        }

        if (model.EditedPostId.HasValue && model.EditedPostId > 0) {
            await _postPublishingService.EditPostAsync(model.PostDto, model.EditedPostId.Value);
            _logger.LogInformation("Post {0}: successfully edited", model.EditedPostId);
            TempData["Message"] = "Post edited successfully";
        } else {
            var post = await _postPublishingService.AddPostAsync(model.PostDto);
            _logger.LogInformation("Post {0}: successfully added", post.Id);
            TempData["Message"] = "Post added successfully";
        }
    
        return RedirectToAction("Index");
    }

    [HttpDelete]
    [Route("PostPublishing/Delete")] 
    public async Task<IActionResult> DeleteAsync(int postId)
    {
        if (postId is > 0) {
            await _postPublishingService.DeletePostAsync(postId);
            _logger.LogInformation("Post {0}: successfully deleted", postId);
            TempData["Message"] = "Post deleted successfully";
        } else {
            TempData["Warning"] = "Post id is not valid";            
        }
        return RedirectToAction("Index");
    }
}