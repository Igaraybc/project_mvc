
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
[Authorize("Admin")]
public class AdminController: Controller
{
    public IActionResult Index(){
        return View();
    }
}