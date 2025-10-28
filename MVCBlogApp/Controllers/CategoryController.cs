using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Interface;
using MVCBlogApp.Models.DTO.Category;

namespace MVCBlogApp.Controllers;

public class CategoryController : Controller
{
    private readonly ICategoryManagmentService _service;
    public CategoryController(ICategoryManagmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new AddOrEditCategoryDto());
    }

    [HttpPost]
    public async Task<IActionResult> Index(AddOrEditCategoryDto categoryToAddDto) 
    {
        await _service.AddCategory(categoryToAddDto);
        return RedirectToAction("Index");
    }

    [HttpPut]
    public async Task<IActionResult> EditCategoryAsync(AddOrEditCategoryDto categoryToEditDto, int categoryId)
    {
        await _service.EditCategory(categoryToEditDto, categoryId);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategoryAsync(int categoryId)
    {
        await _service.Delete(categoryId);
        return Ok();
    }
}