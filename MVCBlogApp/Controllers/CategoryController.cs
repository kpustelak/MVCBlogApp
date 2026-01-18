using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Interface;
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
        return View(new CategoryIndex(await _publicService.GetCategoriesAsync()));
    }

    [HttpGet]
    [Route("Category/AddOrEdit/{id?}")]
    public async Task<IActionResult> AddOrEditView(int? id)
    {
        if (id != null )
        {
            var category = await _publicService.GetCategoryByIdAsync(id.Value);
            if (category != null)
            {
                var vm = new AddOrEditCategoryDto{Name =  category.Name, IconBootstrapLink =  category.IconBootstrapLink};
                return View(new EditCategoryView(id.Value,vm));
            }
        }
        return View(new EditCategoryView(null,null));
        
    }
    
    [HttpPost]
    [Route("Category/AddOrEdit")]
    public async Task<IActionResult> AddOrEdit([FromForm]EditCategoryView categoryIndex)
    {
        if (!ModelState.IsValid)
        {
            throw new ArgumentException("Validation Error");
        }
        try
        {
            if (categoryIndex.CategoryId > 0 && categoryIndex.CategoryDto != null)
            {
                await _service.EditCategory(categoryIndex.CategoryDto, categoryIndex.CategoryId.Value);
                _logger.LogInformation($"Category with id: {categoryIndex.CategoryId} was edited");
            }
            else
            {
                await _service.AddCategory(categoryIndex.CategoryDto);
            }
        }catch(Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    [Route("Category/delete")]
    public async Task<IActionResult> DeleteCategoryAsync(int id)
    {
        try
        {
            await _service.Delete(id);
            _logger.LogInformation($"Category with id: {id} was deleted");
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}