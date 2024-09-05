using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;

[Area("Admin")]
[Authorize("Admin")]
public class AdminOrderController : Controller
{
    private readonly AppDbContext _context;

    public AdminOrderController(AppDbContext context)
    {
        _context = context;
    }

    // GET: AdminOrder
    /* public async Task<IActionResult> Index()
    {
        return View(await _context.Orders.ToListAsync());
    } */

    public async Task<IActionResult> Index(string filter, int pageindex=1, string sort = "FirstName"){
        var resultado = _context.Orders.AsNoTracking().AsQueryable();

        if(!string.IsNullOrEmpty(filter)){
            resultado = resultado.Where(p => p.FirstName.Contains(filter));
        }
        //5 => quantidade de itens por página
        var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "FirstName");
        model.RouteValue = new RouteValueDictionary{{"filter", filter}};

        return View(model);
    }

    // GET: AdminOrder/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _context.Orders
            .FirstOrDefaultAsync(m => m.OrderId == id);
        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    // GET: AdminOrder/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: AdminOrder/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("OrderId,FirstName,LastName,Address1,Address2,Cep,State,City,Phone,Email,OrderSent,OrderDeliveredIn")] Order order)
    {
        if (ModelState.IsValid)
        {
            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(order);
    }

    // GET: AdminOrder/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return View(order);
    }

    // POST: AdminOrder/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("OrderId,FirstName,LastName,Address1,Address2,Cep,State,City,Phone,Email,OrderSent,OrderDeliveredIn")] Order order)
    {
        if (id != order.OrderId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(order);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.OrderId))
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
        return View(order);
    }

    // GET: AdminOrder/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var order = await _context.Orders
            .FirstOrDefaultAsync(m => m.OrderId == id);
        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }

    // POST: AdminOrder/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult OrderSnack(int? id){
        var order = _context.Orders.Include(o => o.OrderItems).ThenInclude(s => s.Snack).FirstOrDefault(o => o.OrderId == id);

        if(order == null){
            Response.StatusCode = 404;
            return View("OrderNotFound", id.Value);
        }

        OrderSnackViewModel orderSnacks = new() {
            Order = order,
            DetailOrders = order.OrderItems
        };

        return View(orderSnacks);
    }

    private bool OrderExists(int id)
    {
        return _context.Orders.Any(e => e.OrderId == id);
    }
}

