using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

[Area("Admin")]
[Authorize("Admin")]
public class AdminSnackController : Controller
{
    private readonly AppDbContext _context;

    public AdminSnackController(AppDbContext context)
    {
        _context = context;
    }

    // GET: AdminSnack
    /* public async Task<IActionResult> Index()
    {
        var appDbContext = _context.Snacks.Include(s => s.Category);
        return View(await appDbContext.ToListAsync());
    }
 */
    public async Task<IActionResult> Index(string filter, int pageindex=1, string sort = "SnackName"){
        var resultado = _context.Snacks.AsNoTracking().AsQueryable();

        if(!string.IsNullOrEmpty(filter)){
            resultado = resultado.Where(p => p.SnackName.Contains(filter));
        }
        //5 => quantidade de itens por página
        var model = await PagingList.CreateAsync(resultado, 3, pageindex, sort, "SnackName");
        model.RouteValue = new RouteValueDictionary{{"filter", filter}};

        return View(model);
    }
    // GET: AdminSnack/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var snack = await _context.Snacks
            .Include(s => s.Category)
            .FirstOrDefaultAsync(m => m.SnackId == id);
        if (snack == null)
        {
            return NotFound();
        }

        return View(snack);
    }

    // GET: AdminSnack/Create
    public IActionResult Create()
    {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
        return View();
    }

    // POST: AdminSnack/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("SnackId,SnackName,ShortDescription,LongDescription,Price,ImageUrl,ImageThumbnailUrl,IsFavoriteSnack,InStock,CategoryId")] Snack snack)
    {
        if (ModelState.IsValid)
        {
            _context.Add(snack);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", snack.CategoryId);
        return View(snack);
    }

    // GET: AdminSnack/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var snack = await _context.Snacks.FindAsync(id);
        if (snack == null)
        {
            return NotFound();
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", snack.CategoryId);
        return View(snack);
    }

    // POST: AdminSnack/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("SnackId,SnackName,ShortDescription,LongDescription,Price,ImageUrl,ImageThumbnailUrl,IsFavoriteSnack,InStock,CategoryId")] Snack snack)
    {
        if (id != snack.SnackId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(snack);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SnackExists(snack.SnackId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", snack.CategoryId);
        return View(snack);
    }

    // GET: AdminSnack/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var snack = await _context.Snacks
            .Include(s => s.Category)
            .FirstOrDefaultAsync(m => m.SnackId == id);
        if (snack == null)
        {
            return NotFound();
        }

        return View(snack);
    }

    // POST: AdminSnack/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var snack = await _context.Snacks.FindAsync(id);
        if (snack != null)
        {
            _context.Snacks.Remove(snack);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SnackExists(int id)
    {
        return _context.Snacks.Any(e => e.SnackId == id);
    }
}

