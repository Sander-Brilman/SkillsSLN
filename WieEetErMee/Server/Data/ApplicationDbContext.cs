using Microsoft.EntityFrameworkCore;
using WieEetErMee.Server.Data.Models;

namespace WieEetErMee.Server.Data;

public class ApplicationDbContext : DbContext   
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
            
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasData(new List<User>()
            {
                new User()
                {
                    Name = "Sander",
                    DefaultPresentOnFriday = true,
                    DefaultPresentOnMonday = true,
                    DefaultPresentOnSaturday = true,
                    DefaultPresentOnSunday = true,
                    DefaultPresentOnThursday = true,
                    DefaultPresentOnTuesday = true,
                    DefaultPresentOnWednesday = true,
                },

                new User()
                {
                    Name = "Jasper",
                    DefaultPresentOnFriday = true,
                    DefaultPresentOnMonday = false,
                    DefaultPresentOnSaturday = true,
                    DefaultPresentOnSunday = true,
                    DefaultPresentOnThursday = false,
                    DefaultPresentOnTuesday = false,
                    DefaultPresentOnWednesday = false,
                },
            });


    }

    public DbSet<User> Users { get; set; }

    public DbSet<UserPresence> UserPresense { get; set; }

    public DbSet<Day> Days { get; set; }
}
