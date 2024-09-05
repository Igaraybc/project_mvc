using System.Reflection.PortableExecutable;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
public class AdminSalesReportController: Controller
{
    private readonly SalesReportService salesReportService;

    public AdminSalesReportController(SalesReportService salesReportService)
    {
        this.salesReportService = salesReportService;
    }

    public IActionResult Index(){
        return View();
    }

    public async Task<IActionResult> SalesReportSimple(DateTime? minDate, DateTime? maxDate){
        if(!minDate.HasValue){
            minDate = new DateTime(DateTime.UtcNow.Year, 1, 1);
        }
        if(!maxDate.HasValue){
            maxDate = DateTime.UtcNow;
        }

        ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
        ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

        var result = await salesReportService.FindByDateAsync(minDate, maxDate);
        return View(result);
    }
}