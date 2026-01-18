using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace MVCBlogApp.Filters;

public class HandleExceptionAttribute : ExceptionFilterAttribute
{
    
    public override void OnException(ExceptionContext context)
    {
        var logger = context.HttpContext.RequestServices
            .GetRequiredService<ILogger<HandleExceptionAttribute>>();
        
        if (context.ExceptionHandled)
            return;
        
        var factory = context.HttpContext.RequestServices
            .GetService(typeof(ITempDataDictionaryFactory)) as ITempDataDictionaryFactory;
        var tempData = factory?.GetTempData(context.HttpContext);
    
        bool isLightError = context.Exception is 
            ArgumentException or 
            InvalidOperationException or
            ValidationException;  

        if (isLightError)
        {
            if (tempData != null)
            {
                tempData["ErrorMessage"] = context.Exception.Message;
            }

            context.ExceptionHandled = true;
            logger.LogError(context.Exception, "Light error occurred: {Message}", context.Exception.Message);
        
            var returnUrl = context.HttpContext.Request.Headers["Referer"].ToString();
            context.Result = new RedirectResult(string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl);
        }
        else
        {
            if (tempData != null)
            {
                tempData["ErrorMessage"] = "There is some serious problem. Please try again later.";
                tempData["StatusCode"] = 500;
            }
        
            logger.LogCritical(context.Exception, "Critical error occurred: {Message}", context.Exception.Message);
        
            context.ExceptionHandled = true;
            context.Result = new RedirectToActionResult("Index", "Home", new { statusCode = 500 });
        }
    }
}