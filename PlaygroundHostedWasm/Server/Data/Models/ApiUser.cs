using Microsoft.AspNetCore.Identity;

namespace PlaygroundHostedWasm.Server.Data.Models;

public class ApiUser : IdentityUser
{
    public string FirstName { get; set; }
}
