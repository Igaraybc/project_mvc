
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class ContactController: Controller
{
    public IActionResult Index(){
        return View();
    }
}