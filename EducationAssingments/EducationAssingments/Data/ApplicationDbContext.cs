using EducationAssingments.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EducationAssingments.Data;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Assingment> Assingments { get; set; }

    public DbSet<AssingmentAnswer> AssingmentAnswers { get; set; }
}
