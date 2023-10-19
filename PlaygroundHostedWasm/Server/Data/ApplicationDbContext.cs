using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlaygroundHostedWasm.Server.Data.Models;

namespace PlaygroundHostedWasm.Server.Data;

public class ApplicationDbContext : IdentityDbContext<ApiUser>
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
        
    }

}
