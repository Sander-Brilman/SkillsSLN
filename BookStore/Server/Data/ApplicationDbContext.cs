global using BookStore.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Server.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
            
    }

    public DbSet<Book> Book { get; set; }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Category> Categories { get; set; }
}
