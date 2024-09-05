namespace project_mvc.ViewModels;

public class SnackListViewModel
{
    public IEnumerable<Snack> Snacks { get; set; } = [];
    public string CurrentCategory { get; set; } = string.Empty;
}