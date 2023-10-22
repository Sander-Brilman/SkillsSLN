using Microsoft.AspNetCore.Identity;

namespace PersonalNotes.Server.Data.Models;

public class ApplicationUser : IdentityUser
{
    public List<Journal> Journals { get; set; } = new();
}
