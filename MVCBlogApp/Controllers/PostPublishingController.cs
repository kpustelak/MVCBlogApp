using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Interface;
using MVCBlogApp.Models.DTO.Post;
using MVCBlogApp.Models.ViewModel.Post;
using ImageIndex = MVCBlogApp.Models.ViewModel.Image.ImageIndex;

namespace MVCBlogApp.Controllers;

public class PostPublishingController : Controller
{
    private readonly IPostPublishingService _postPublishingService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    private readonly ILogger<PostPublishingController> _logger;
    
    public PostPublishingController(
        IPostPublishingService postPublishingService,
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
    [Route("PostPublishing")]
    public async Task<IActionResult> Index(int page = 1)
    {
        try
        {
            var pageSize = 20;
            var totalItems = await _postPublishingService.GetTotalPostCountAsync();
            var posts = await _postPublishingService.GetPostDataListAsync(page, pageSize);

            var vm = new PostIndex
            {
                Posts = posts,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
                CurrentPage = page
            };
            return View(vm);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    
    [HttpGet]
    [Route("PostPublishing/Edit")] 
    public async Task<IActionResult> Edit(int? postId) 
    {
        var viewModel = new EditPostView 
        { 
            PostCategories = await _categoryService.GetCategoriesAsync() 
        };
        
        if (postId.HasValue && postId > 0) 
        {
            var post = await _postPublishingService.GetWholePostAsync(postId.Value);
            viewModel.EditedPostId = postId;
            viewModel.PostDto = _mapper.Map<AddOrEditPostDto>(post);
        }
        
        return View(viewModel);
    }
    
    [HttpPost]
    [Route("PostPublishing/Save")] 
    public async Task<IActionResult> Save([FromForm] EditPostView model)
    {
        if (!ModelState.IsValid) 
        {
            throw new ArgumentException("Invalid form data. Please check all required fields.");
        }

        if (model.EditedPostId.HasValue && model.EditedPostId > 0 ) 
        {
            try
            {
                await _postPublishingService.EditPostAsync(model.PostDto, model.EditedPostId.Value);
                _logger.LogInformation("Post {PostId} successfully edited", model.EditedPostId);
                TempData["SuccessMessage"] = "Post edited successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        else 
        {
            try
            {
                var post = await _postPublishingService.AddPostAsync(model.PostDto);
                _logger.LogInformation("Post {PostId} successfully added", post.Id);
                TempData["SuccessMessage"] = "Post added successfully";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        return RedirectToAction("Index", "PostPublishing");
    }

    [HttpPost]
    [Route("PostPublishing/Delete")] 
    public async Task<IActionResult> Delete(int postId)
    {
        if (postId <= 0) 
        {
            throw new ArgumentException("Invalid post ID");
        }

        try
        {
            await _postPublishingService.DeletePostAsync(postId);
            _logger.LogInformation("Post {PostId} successfully deleted", postId);
            TempData["SuccessMessage"] = "Post deleted successfully";
            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("PostPublishing/Favourite")]
    public async Task<IActionResult> Favourite(int postId)
    {
        if (postId <= 0) 
        {
            throw new ArgumentException("Invalid post ID");
        }

        try
        {
            await _postPublishingService.ChangeFavoritePostAsync(postId);
            _logger.LogInformation("Post {PostId} has new status.", postId);
            TempData["SuccessMessage"] = "Post added to /removed from favourite successfully";
            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
        return RedirectToAction("Index");
    }
}