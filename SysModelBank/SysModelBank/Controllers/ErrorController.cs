using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
public class ErrorController : Controller 
{   
    [HttpGet]
    public IActionResult AccessDenied() 
    {
        return View();
    }
}