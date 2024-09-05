namespace project_mvc;

public interface ISnackRepository
{
    IEnumerable<Snack> Snacks { get; }
    IEnumerable<Snack> FavoritesSnacks {get;}
    Snack GetSnackById(int id);
}
