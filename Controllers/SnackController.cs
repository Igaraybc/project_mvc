using Microsoft.AspNetCore.Mvc;
using project_mvc;
using project_mvc.ViewModels;
public class SnackController : Controller
{
    private readonly ISnackRepository _snackRepository;

    public SnackController(ISnackRepository snackRepository)
    {
        _snackRepository = snackRepository;
    }

    public IActionResult List(string category)
    {
        IEnumerable<Snack> snacks;
        string currentCategory = string.Empty;

        if (string.IsNullOrEmpty(category)){
            snacks = _snackRepository.Snacks.OrderBy(s => s.SnackId);
            currentCategory = "Todos os lanches";
        }
        else{
            snacks = _snackRepository.Snacks.Where(s => s.Category.CategoryName.ToLower().Equals(category.ToLower())).OrderBy(s => s.SnackName);
            if(snacks.Any()){
                currentCategory = category;
            }
            else{
                currentCategory = "none";
                snacks = _snackRepository.Snacks.OrderBy(s => s.SnackId);
            }
        }

        var snackViewModel = new SnackListViewModel{
            Snacks = snacks,
            CurrentCategory = currentCategory 
        };

        return View(snackViewModel);
        
    }

    public IActionResult Details(int snackId){
        var snack = _snackRepository.Snacks.FirstOrDefault(s => s.SnackId == snackId);
        return View(snack);
    }

    public IActionResult Search(string searchString){
        IEnumerable<Snack> snacks;
        string currentCategory = string.Empty;

        if(string.IsNullOrEmpty(searchString)){
            snacks = _snackRepository.Snacks.OrderBy(s => s.SnackId);
            currentCategory = "Todos os lanches";
        }    
        else{
            snacks = _snackRepository.Snacks.Where(s => s.SnackName.ToLower().Contains(searchString.ToLower()));

            if(snacks.Any()){
                currentCategory = $"Resultados da busca: \"{searchString}\"";
            }
            else{
                currentCategory = "none";
                snacks = _snackRepository.Snacks.OrderBy(s => s.SnackId);
            }
        }
        return View("~/Views/Snack/List.cshtml", new SnackListViewModel {
            Snacks = snacks,
            CurrentCategory = currentCategory
        });
    }

}