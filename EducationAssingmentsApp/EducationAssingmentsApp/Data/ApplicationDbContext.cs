using EducationAssingmentsApp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EducationAssingmentsApp.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        IdentityRole teacherRole = new("Admin")
        {
            NormalizedName = "ADMIN",
        };

        IdentityRole studentRole = new("Student")
        {
            NormalizedName = "STUDENT"
        };

        builder.Entity<IdentityRole>()
            .HasData([teacherRole, studentRole]);


        base.OnModelCreating(builder);
    }


    public DbSet<Assingment> Assingments { get; set; }
    public DbSet<AssingmentAnswer> AssingmentsAnswers { get; set; }
}
