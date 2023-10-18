using Microsoft.AspNetCore.Identity;

namespace AuthWithApi.Data.Models;

public class ApiUser : IdentityUser
{
    public string FirstName { get; set; } = "";
}
