using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MVCBlogApp.Controllers;

public class BlogPagesController : Controller
{
    public BlogPagesController()
    {

    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}