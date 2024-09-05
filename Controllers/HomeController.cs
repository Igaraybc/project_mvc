using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project_mvc.Models;

namespace project_mvc.Controllers;

public class HomeController : Controller
{
    private readonly ISnackRepository _snackRepository;

    public HomeController(ISnackRepository snackRepository)
    {
        _snackRepository = snackRepository;
    }

    public IActionResult Index()
    {
        var homeVM = new HomeViewModel{
            FavoritesSnacks = _snackRepository.FavoritesSnacks
        };

        return View(homeVM);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
