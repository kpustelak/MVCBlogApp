using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Interface;
using MVCBlogApp.Models.DTO.Category;
using MVCBlogApp.Models.ViewModel.Category;

namespace MVCBlogApp.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryManagmentService _service;
    private readonly ICategoryService _publicService;
    private readonly ILogger<CategoryController> _logger;
    public CategoryController(ICategoryManagmentService service,
                                ICategoryService publicService,
                                ILogger<CategoryController> logger)
    {
        _service = service;
        _publicService = publicService;
        _logger = logger;
    }

    [HttpGet]
    [Route("Category")]
    public async Task<IActionResult> Index()
    {
        return View(new CategoryIndex(await _publicService.GetCategories(),null,null));
    }

    [HttpPut]
    [Route("Category")]
    public async Task<IActionResult> AddOrEdit([FromForm]CategoryIndex categoryIndex)
    {
        if (!ModelState.IsValid)
        {
            throw new ArgumentException("Invalid form data. Please check all required fields.");
        }
        try
        {
            if (categoryIndex.CategoryId > 0 )
            {
                await _service.EditCategory(categoryIndex.CategoryToEdit, categoryIndex.CategoryId.Value);
                _logger.LogInformation($"Category with id: {categoryIndex.CategoryId} was edited");
            }
            else
            {
                await _service.AddCategory(categoryIndex.CategoryToEdit);
            }
        }catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return RedirectToAction("Index");
    }

    [HttpDelete]
    [Route("Category/{id}")]
    public async Task<IActionResult> DeleteCategoryAsync(int id)
    {
        await _service.Delete(id);
        return Ok();
    }
}