using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
[Authorize("Admin")]
public class AdminChartController: Controller
{
    private readonly SalesChartService _salesChartService;

    public AdminChartController(SalesChartService salesChartService)
    {
        _salesChartService = salesChartService;
    }

    public JsonResult SnackSales(int days){
        var snackTotalSales = _salesChartService.GetSnackSales(days);
        return Json(snackTotalSales);
    }

    public IActionResult Index(){

        return View();
    }

    public IActionResult MonthlySales(){

        return View();
    }

    public IActionResult WeeklySales(){

        return View();
    }
}