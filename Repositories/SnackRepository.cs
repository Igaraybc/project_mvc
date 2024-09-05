
using Microsoft.EntityFrameworkCore;

namespace project_mvc;

public class SnackRepository: ISnackRepository
{
    private readonly AppDbContext _context;

    public SnackRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Snack> Snacks => _context.Snacks.Include(c => c.Category);

    public IEnumerable<Snack> FavoritesSnacks => _context.Snacks.Where(p => p.IsFavoriteSnack).Include(c => c.Category);

    public Snack GetSnackById(int id) => _context.Snacks.FirstOrDefault(snack => snack.SnackId == id);
}
