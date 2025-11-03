using Microsoft.AspNetCore.Mvc;

namespace MVCBlogApp.Controllers;

public class ErrorController : Controller
{
    [Route("Error/{statusCode}")]
    public IActionResult Index(int statusCode)
    {
        ViewBag.StatusCode = statusCode;
        ViewBag.ErrorMessage = TempData["ErrorMessage"]?.ToString();
        
        ViewBag.Title = statusCode switch
        {
            404 => "Page Not Found",
            401 => "Unauthorized",
            403 => "Forbidden",
            500 => "Server Error",
            _ => "Error"
        };
        
        ViewBag.Description = statusCode switch
        {
            404 => "The page you're looking for doesn't exist.",
            401 => "You need to log in to access this resource.",
            403 => "You don't have permission to access this resource.",
            500 => "Something went wrong on our end.",
            _ => "Please try again later."
        };
        
        return View();
    }
}