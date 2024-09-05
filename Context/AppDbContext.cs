using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set;}
    public DbSet<Snack> Snacks { get; set;}
    public DbSet<ShoppingCartItem> ShoppingCartItems { get; set;}

    public DbSet<Order> Orders { get; set;}
    public DbSet<DetailOrder> DetailOrders { get; set;}

}
