using Microsoft.EntityFrameworkCore;
using ShopApp.Models;

namespace ShopApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Shop> Shops => Set<Shop>();
}
