using Microsoft.EntityFrameworkCore;
using ProductExport.Server.Data.Models;

namespace ProductExport.Server.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
            
    }

    public DbSet<Product> Product { get; set; }

    public DbSet<Category> Category { get; set; }
}
